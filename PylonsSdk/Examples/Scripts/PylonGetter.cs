using UnityEngine;
using PylonsSdk.ProfileTools;
using System;

namespace PylonsSdk.Examples
{
    /// <summary>
    /// This example gets 99 Pylons from the relevant endpoint.
    /// (If the available get-pylons endpoint doesn't support that kind of granularity,
    /// we should automatically get whatever the smallest available amount greater than 99 is.)
    /// </summary>
    public class PylonGetter : MonoBehaviour
    {
        public int Quantity;
        public bool Busy;

        public void GetPylons ()
        {
            Busy = true;
            var startingPylons = 0L;
            var endingPylons = 0L;
            Profile.GetSelf<Profile>((s, p) => {
                startingPylons = p.Profile.GetCoin("pylon");
            });
            PylonsService.instance.GetPylons(Quantity, (s, t) =>
            {
                //System.Threading.Thread.Sleep(5000);
                // Because the IPC interface processes calls first-in first-out and runs only one operation at a time,
                // it's not necessary to deal w/ nested event hierarchies. These three operations will always run in
                // sequence, one after the other.
                // TO-DO: Ensuring deterministic order here *does* require these calls to be done off of the main thread.
                // Unsure of where the best place to do that check is ATM, though. IPC manager, maybe?
                Debug.Log(t[0].Code);
                Debug.Log("getpylons");
            });
            
            Profile.GetSelf<Profile>((s, p) => {
                endingPylons = p.Profile.GetCoin("pylon");
                if (startingPylons + Quantity > endingPylons) throw new Exception($"GetPylons operation failed! Got {endingPylons - startingPylons} pylons");
                else Debug.Log($"Got {endingPylons - startingPylons} pylons");
                Busy = false;
            });
        }
    }
}