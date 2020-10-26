using Newtonsoft.Json;
using PylonsIpc;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PylonsSdk.ProfileTools.Editor
{
    public delegate void ProfileDefEvent(object caller, ProfileDef profileDef, Profile profile);

    public readonly struct ProfileDef : IEquatable<ProfileDef>
    {
        private delegate void KeyEvent(string name, string address, string privateKey, string publicKey);

        public readonly string PrivateKey;
        public readonly string PublicKey;
        /// <summary>
        /// NOTE: names aren't actually supported properly on the backend yet...
        /// </summary>
        public readonly string Name;
        public readonly string Address;
        public readonly string Note;

        public ProfileDef(string privateKey, string publicKey, string name, string address, string note)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
            Name = name;
            Address = address;
            Note = note;
        }

        private static void RetrieveKeyInfo(KeyEvent evt)
        {
            // We purposefully don't expose ExportKeys to PylonsService, so we have to do this the ll way
            IpcInteraction.Stage(() => new ExportKeys(), (s, e) => { evt.Invoke(((KeyResponse)e).Name, ((KeyResponse)e).Address, ((KeyResponse)e).PrivateKey, ((KeyResponse)e).PublicKey); });
        }

        /// <summary>
        /// Returns a ProfileDef object referring to the profile presently being used by the wallet.
        /// If one doesn't exist, creates one.
        /// </summary>
        public static void GetActive(ProfileDefEvent evt)
        {
            void cb()
            {
                Profile.GetSelf<Profile>((s, p) =>
                {
                    foreach (var def in GetAll())
                    {
                        if (p.Profile.Address == def.Address)
                        {
                            evt.Invoke(null, def, p.Profile);
                            Debug.Log(p.Profile.Address);
                            break;
                        }
                    }
                    // if we didn't return anything, it doesn't exist, so we have to make it
                    RetrieveKeyInfo((name, address, priv, pub) => { evt.Invoke(null, new ProfileDef(priv, pub, name, address, ""), p.Profile); });
                    Debug.Log("no address");
                });
            }
            if (PylonsService.instance == null) PylonsService.onServiceLive += (s, e) => { cb(); };
            else cb();
        }

        public static ProfileDef[] GetAll()
        {
            var ls = new List<ProfileDef>();
            var path = Path.Combine(Application.persistentDataPath, "profiles");
            if (Directory.Exists(path)) foreach (var f in Directory.GetFiles(path)) ls.Add(JsonConvert.DeserializeObject<ProfileDef>(File.ReadAllText(f)));
            return ls.ToArray();
        }

        public void Delete()
        {
            var fn = Path.Combine(Application.persistentDataPath, "profiles", $"{Name}{Address}");
            if (File.Exists(fn)) File.Delete(fn);
        }

        public void Save()
        {
            if (!Valid) throw new Exception("Shouldn't be trying to save a ProfileDef that doesn't represent a valid set of keys!");
            //else if (string.IsNullOrEmpty(Name)) throw new Exception("Shouldn't be trying to save a profile with no name.");
            var fn = Path.Combine(Application.persistentDataPath, "profiles", $"{Name}{Address}");
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "profiles"));
            File.WriteAllText(fn, JsonConvert.SerializeObject(this));
        }

        public bool Valid => !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(PrivateKey) && !string.IsNullOrEmpty(PublicKey);

        public void CheckExists(Action<bool> evt)
        {
            if (!Valid) evt.Invoke(false);
            else PylonsService.instance.GetProfile(Address, (s, p) => { evt.Invoke(!p.Default); });
        }

        public bool Equals(ProfileDef other)
        {
            return PrivateKey == other.PrivateKey &&
                   PublicKey == other.PublicKey &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Note == other.Note;
        }

        public override int GetHashCode()
        {
            var hashCode = -993932415;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrivateKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PublicKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            hashCode = hashCode * -1521134295 + Valid.GetHashCode();
            return hashCode;
        }
    }
}