using UnityEngine;
using PylonsSdk.ProfileTools;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// Example that retrieves the current state of an arbitrary profile and
    /// sets a value based on the number of pylons it holds.
    /// </summary>
    public class GetsOtherProfile : MonoBehaviour
    {
        public string Address;
        public bool Finished;
        public long Pylons;

        void Start()
        {
            Profile.Get<Profile>(Address, (s, p) => {
                Pylons = p.Profile.GetCoin("pylon");
                Finished = true;
            });
        }
    }
}