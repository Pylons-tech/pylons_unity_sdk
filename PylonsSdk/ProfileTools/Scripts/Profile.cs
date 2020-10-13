using System;
using System.Reflection;

namespace PylonsSdk.ProfileTools
{
    public class ProfileEventArgs<T> : EventArgs where T : Profile
    {
        public readonly T Profile;

        public ProfileEventArgs(T profile)
        {
            Profile = profile;
        }
    }

    public class Profile
    {
        private static T ReflectiveInstantiate<T>(Tx.Profile prf) where T : Profile
        {
            if (!(typeof(T).GetConstructor(new Type[] { typeof(Tx.Profile) })?.Invoke(new object[] { prf }) is T output))
                throw new Exception($"Failed to instantiate ProfileTools.Profile subclass {typeof(T).Name}. Does it have a constructor that accepts an argument of Tx.Profile?");
            return output;
        }

        public static void GetSelf<T>(EventHandler<ProfileEventArgs<T>> eventHandler) where T : Profile => 
            PylonsService.instance.GetProfile("", (s, p) => { eventHandler.Invoke(s, new ProfileEventArgs<T>(ReflectiveInstantiate<T>(p))); });
        public static void Get<T>(string addr, EventHandler<ProfileEventArgs<T>> eventHandler) where T : Profile =>
            PylonsService.instance.GetProfile(addr, (s, p) => { eventHandler.Invoke(s, new ProfileEventArgs<T>(ReflectiveInstantiate<T>(p))); });

        private long lastHeight;
        private Tx.Profile last;
        public string Name => last.Name;
        public string Address => last.Address;
        public Tx.Coin[] Coins => last.Coins;
        public Tx.Item[] Items => last.Items;

        public Profile (Tx.Profile prf)
        {
            last = prf;
            lastHeight = StatusBlock.Last.BlockHeight;
        }

        private void Update ()
        {
            if (PylonsService.instance == null) throw new PylonsUtilSceneNotLoadedException();
            else PylonsService.instance.GetProfile("", (s, p) =>
            {
                last = p;
                lastHeight = StatusBlock.Last.BlockHeight;
            }); 
        }

        public long GetCoin(string denom)
        {
            foreach (var coin in last.Coins) if (coin.Denom == denom) return coin.Amount;
            return 0;
        }

#if PYLONSSDK_ITEMSCHEMA_RUNTIME
        public T[] Items<T> () where T : ItemSchema.ItemSchema, new()
        {
            var ls = new List<T>();
            foreach (var item in last.Items) if (ItemSchema.ItemSchema.FitSchema<T>(item, out var schematized)) ls.Add(schematized);
            return ls.ToArray();
        }
#endif
    }
}