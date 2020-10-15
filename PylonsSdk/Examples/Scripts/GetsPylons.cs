using UnityEngine;
using PylonsSdk.ProfileTools;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// This example gets 99 Pylons from the relevant endpoint.
    /// (If the available get-pylons endpoint doesn't support that kind of granularity,
    /// we should automatically get whatever the smallest available amount greater than 99 is.)
    /// </summary>
    public class GetsPylons : MonoBehaviour
    {
        public bool Finished;
        public long StartingPylons;
        public long CurrentPylons;

        void Start ()
        {
            Profile.GetSelf<Profile>((s, p) => {
                StartingPylons = p.Profile.GetCoin("pylon");
            });
            PylonsService.instance.GetPylons(99, (s, p) =>
            {
                // Because the IPC interface processes calls first-in first-out and runs only one operation at a time,
                // it's not necessary to deal w/ nested event hierarchies. These three operations will always run in
                // sequence, one after the other.
                // TO-DO: Ensuring deterministic order here *does* require these calls to be done off of the main thread.
                // Unsure of where the best place to do that check is ATM, though. IPC manager, maybe?
            });
            Profile.GetSelf<Profile>((s, p) => {
                CurrentPylons = p.Profile.GetCoin("pylon");
                Finished = true;
            });
        }
    }
}