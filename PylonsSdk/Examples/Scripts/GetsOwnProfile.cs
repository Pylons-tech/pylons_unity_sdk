using UnityEngine;
using PylonsSdk.ProfileTools;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// Example that retrieves the current state of the user's own profile and
    /// sets a value based on the number of pylons it holds.
    /// </summary>
    public class GetsOwnProfile : MonoBehaviour
    {
        public string Address;
        public bool Finished;
        public long Pylons;

        void Start()
        {
            Profile.GetSelf<Profile>((s, p) => {
                Address = p.Profile.Address;
                Pylons = p.Profile.GetCoin("pylon");
                Finished = true;
            });
        }
    }
}