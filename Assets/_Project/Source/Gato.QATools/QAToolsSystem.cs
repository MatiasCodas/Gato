using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.QATools
{
    public class QAToolsSystem : MonoSystem
    {
        private SRPlayerStatsOptions playerStatsOptions = new SRPlayerStatsOptions();

        public override void LateSetup()
        {
            SRDebug.Instance.AddOptionContainer(playerStatsOptions);
            playerStatsOptions.Initialize();
        }
    }
}
