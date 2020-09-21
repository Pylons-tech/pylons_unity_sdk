using System.Collections.Generic;

namespace PylonsSdk.ProfileTools
{
    public abstract class Profile
    {
        private long lastHeight;
        private Tx.Profile last;

        private void Update ()
        {
            if (PylonsService.instance == null) throw new PylonsUtilSceneNotLoadedException();
            else PylonsService.instance.GetProfile("", (s, p) =>
            {
                last = p;
                lastHeight = StatusBlock.Last.BlockHeight;
            }); 
        }
    }
}