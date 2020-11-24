using PylonsIpc;
using PylonsSdk.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PylonsSdk.ProfileTools.Editor
{
    public class ProfileViewerEditorWindow : EditorWindow
    {
        private enum ProfileExistState
        {
            INDETERMINATE,
            EXISTS,
            DOES_NOT_EXIST
        }

        private bool ready;
        private ProfileDef currentDef;
        private Profile currentProfile;
        private ProfileDef[] profileDefs = new ProfileDef[0];
        private bool showPrivateKey;
        // HACK: Profile names don't exist on the backend yet, but we need a name to assign to profiles for our UI to make sense.
        private string localName;
        private string localNote;
        private ProfileExistState profileExists;
        private Texture2D red;
        private Texture2D yellow;
        private Texture2D green;
        private float width;
        const float normalizedWidth = 256;
        private string nameInput;

        [MenuItem("Pylons/Profile")]
        static void Init() => GetWindow<ProfileViewerEditorWindow>().Show();

        void SetupResources()
        {
            red = new Texture2D(16, 16);
            yellow = new Texture2D(16, 16);
            green = new Texture2D(16, 16);
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    red.SetPixel(x, y, Color.red);
                    yellow.SetPixel(x, y, Color.yellow);
                    green.SetPixel(x, y, Color.green);
                }
            }
            red.Apply(true, true);
            yellow.Apply(true, true);
            green.Apply(true, true);
        }

        void OnEnable()
        {
            SetupResources();
            if (!LoadProfiles()) RegisterNewProfile();
            else SetProfile(profileDefs[0]);
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        }

        // hide before reloading assemblies, since we're gonna lose the devwallet instance
        void OnBeforeAssemblyReload()
        {
            ready = false;
        }

        private bool LoadProfiles()
        {
            profileDefs = ProfileDef.GetAll();
            if (profileDefs.Length > 0)
            {
                // Sort the loaded profiledefs so the current profile is at index 0;
                var index = GetIndexOfCurrent();
                Debug.Log($"Index is {index}");
                if (index != -1)
                {
                    var p = new ProfileDef[profileDefs.Length];
                    p[0] = profileDefs[index];
                    for (int i = 0; i < profileDefs.Length; i++)
                    {
                        if (i < index) p[i + 1] = profileDefs[i];
                        else if (i > index) p[i + 1] = profileDefs[i - 1];
                    }
                    profileDefs = p;
                }
            }
            return profileDefs.Length > 0;
        }

        private void UpdateDataModel ()
        {
            ProfileDef.GetActive((s, def, profile) =>
            {
                SetProfile(def, profile);
                LoadProfiles();
                ready = true;
            });
        }

        void OnGUI()
        {
            if (ready)
            {
                Toolbar();
                ProfileGui(currentDef);
            }
            else
            {
                GUILayout.Label("Loading...\nPlease wait."); // todo: this should be less bad
            }
        }

        private void SetProfile(ProfileDef profileDef) => SetProfile(profileDef, null); 

        private void SetProfile(ProfileDef profileDef, Profile profile)
        {
            Debug.Log("Set profile");
            currentDef = profileDef;
            currentProfile = profile;
            localName = currentDef.Name;
            localNote = currentDef.Note;
            CheckProfileExists(currentDef);
        }

        private float Normalize(float f) => (width / normalizedWidth) * f;

        private void SetWindowSizeVars()
        {
            width = position.size.x;
        }

        private void CheckProfileExists(ProfileDef profileDef)
        {
            profileExists = ProfileExistState.INDETERMINATE;
            profileDef.CheckExists((b => 
            {
                if (b)
                {
                    profileExists = ProfileExistState.EXISTS;
                    profileDef.Save();
                }
                else profileExists = ProfileExistState.DOES_NOT_EXIST;
                ready = true;
            }));
        }

        private void DrawSpriteForProfileExistState()
        {
            // I really hope this compiles into a lookup table...
            // If we ever start seeing UI lag here, this is probably an
            // easy place to claw back some performance, though.
            switch (profileExists)
            {
                case ProfileExistState.INDETERMINATE:
                    GUILayout.Label(yellow);
                    break;
                case ProfileExistState.EXISTS:
                    GUILayout.Label(green);
                    break;
                case ProfileExistState.DOES_NOT_EXIST:
                    GUILayout.Label(red);
                    break;
            }
        }
        
        private void RegisterNewProfile()
        {
            PylonsService.instance.RegisterProfile("", (s, e) => UpdateDataModel());
            ready = false;
            LoadProfiles();
        }

        private void Toolbar()
        {
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                GUIContent[] ps;
                if (profileDefs.Length > 0)
                {
                    ps = new GUIContent[profileDefs.Length];
                    for (int i = 0; i < ps.Length; i++) ps[i] = new GUIContent(profileDefs[i].Name);
                }
                else ps = new GUIContent[] { new GUIContent("Profile") };
                var cur = GetIndexOfCurrent();
                var index = GUILayout.Toolbar(cur, ps);
                if (index != cur) SetProfile(profileDefs[index]); 
                if (GUILayout.Button("+", GUILayout.Width(16))) RegisterNewProfile();
            };
        }

        private void ProfileGui(ProfileDef profileDef)
        {
            SetWindowSizeVars();

            using (var h = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Profile", EditorStyles.boldLabel);
                DrawSpriteForProfileExistState();
            }

            // how to deal w/ the checkexists event...
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField($"Public key: ", EditorStyles.miniLabel, GUILayout.Width(Normalize(96)));
                EditorGUILayout.SelectableLabel(profileDef.PublicKey, EditorStyles.miniTextField);
            };

            using (var h = new EditorGUILayout.HorizontalScope())
            {
                showPrivateKey = EditorGUILayout.Toggle(showPrivateKey, GUILayout.Width(Normalize(26)));
                EditorGUILayout.LabelField($"Private key: ", EditorStyles.miniLabel, GUILayout.Width(Normalize(70)));
                if (showPrivateKey) EditorGUILayout.SelectableLabel(profileDef.PrivateKey, EditorStyles.miniTextField);
                else EditorGUILayout.SelectableLabel("(hidden)", EditorStyles.miniTextField);
            };
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Address: ", EditorStyles.miniBoldLabel, GUILayout.Width(Normalize(96)));
                EditorGUILayout.SelectableLabel(profileDef.Address, EditorStyles.miniTextField);
            };
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Name: ", EditorStyles.miniBoldLabel, GUILayout.Width(Normalize(96)));
                //if (profileExists == ProfileExistState.DOES_NOT_EXIST) nameInput = EditorGUILayout.TextField(nameInput, EditorStyles.miniTextField);
                //else EditorGUILayout.SelectableLabel(profileDef.Name, EditorStyles.miniTextField);
                localName = EditorGUILayout.TextField(localName);
            };

            using (var h = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Notes: ", EditorStyles.miniBoldLabel, GUILayout.Width(Normalize(96)));
                localNote = EditorGUILayout.TextArea(localNote);
            }

            if (localName != profileDef.Name || localNote != profileDef.Note)
            {
                if (GUILayout.Button("Save", GUILayout.Width(Normalize(24))))
                {
                    var index = GetIndexOfCurrent();
                    var newDef = new ProfileDef(currentDef.PrivateKey, currentDef.PublicKey, localName, currentDef.Address, localNote);
                    currentDef.Delete();
                    newDef.Save();
                    profileDefs[index] = newDef;
                    SetProfile(newDef, currentProfile);
                }
            }

            if (profileExists == ProfileExistState.DOES_NOT_EXIST)
            {
                if (GUILayout.Button("Register profile"))
                {
                    profileExists = ProfileExistState.INDETERMINATE;
                    PylonsService.instance.RegisterProfile(nameInput, (s, e) => {
                        UpdateDataModel();
                    });           
                }
            }

            if (currentProfile != null)
            {
                EditorGUILayout.LabelField($"Pylons: {currentProfile.GetCoin("pylon")}", EditorStyles.miniBoldLabel);

                using (var v = new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.LabelField("Other coins", EditorStyles.miniBoldLabel);
                    foreach (var coin in currentProfile.Coins) if (coin.Denom != "pylon") EditorGUILayout.LabelField($"{coin.Denom}: {coin.Amount}", EditorStyles.miniLabel);
                }
                //TODO: also display items
            }
        }

        private int GetIndexOfCurrent()
        {
            for (var i = 0; i < profileDefs.Length; i++) if (profileDefs[i].Equals(currentDef)) return i;
            return -1;
        }
    }

}