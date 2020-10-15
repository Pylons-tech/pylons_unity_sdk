using PylonsSdk.ProfileTools;
using UnityEngine;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// This example executes the recipe created by CreatesRecipe. Obviously, then: it'll fail if
    /// that recipe doesn't presently exist on chain or you don't have at least one pylon.
    /// </summary>
    public class ExecutesRecipe : MonoBehaviour
    {
        public long StartingWidgets;
        public long CurrentWidgets;
        public bool Finished;
        public bool Succeeded;

        void Start ()
        {
            Profile.GetSelf<Profile>((s, p) => {
                StartingWidgets = p.Profile.GetCoin("widget");
            });
            PylonsService.instance.ApplyRecipe("testRecipe", "testCookbook", new string[0], (s, t) =>
            {
                if (t.Length > 0 && t[0].Code == Tx.ResponseCode.OK) Succeeded = true;
            });
            Profile.GetSelf<Profile>((s, p) => {
                CurrentWidgets = p.Profile.GetCoin("widget");
                Finished = true;
            });
        }
    }
}