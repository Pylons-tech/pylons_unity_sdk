using Newtonsoft.Json;
using PylonsSdk.Tx;
using UnityEngine;
using Profile = PylonsSdk.ProfileTools.Profile;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// Example that creates a recipe using the PylonsService calls.
    /// ATM this is how you have to do it, though it's fiddly and obnoxious.
    /// This will be deprecated as a public-facing example once RecipeBuilder is online.
    /// </summary>
    public class CreatesRecipe : MonoBehaviour
    {
        public long StartingPylons;
        public long CurrentPylons;
        public bool Finished;
        public bool Succeeded;

        void Start()
        {
            Profile.GetSelf<Profile>((s, p) => {
                StartingPylons = p.Profile.GetCoin("pylon");
            });
            // These types might be wrong. To-do: an example that actually has outputs.
            // (Hand-rolling outputs is going to be a pain, but this should double nicely as
            // a warning to just use the recipe builder...)
            PylonsService.instance.CreateRecipes(
                names: new string[] { "testRecipe" },
                cookbooks: new string[] { "testCookbook" },
                descriptions: new string[] { "Description for test recipe" },
                blockIntervals: new long[] { 0 },
                coinInputs: new string[] { JsonConvert.SerializeObject(new CoinInput[] { new CoinInput("pylon", 1) }) },
                itemInputs: new string[] { JsonConvert.SerializeObject(new ItemInput[0]) },
                outputTables: new string[] { JsonConvert.SerializeObject(new EntriesList[0]) },
                outputs: new string[] { JsonConvert.SerializeObject(new WeightedOutput[0]) },
                evt: (s, t) =>
                {
                    if (t.Length > 0 && t[0].Code == Tx.ResponseCode.OK) Succeeded = true;
                });
            Profile.GetSelf<Profile>((s, p) => {
                CurrentPylons = p.Profile.GetCoin("pylon");
                Finished = true;
            });
        }
    }
}