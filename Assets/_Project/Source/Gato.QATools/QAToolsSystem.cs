using Gato.Gameplay;
using UnityEngine;

namespace Gato.QATools
{
    public class QAToolsSystem : MonoSystem
    {
        [SerializeField]
        private BasicEnemyStats _basicEnemyStats;
        [SerializeField]
        private BullStats _bullStats;
        [SerializeField]
        private StraferStats _straferStats;
        [SerializeField]
        private StraferStats _mosquitoStats;

        private SRPlayerStatsOptions playerStatsOptions = new SRPlayerStatsOptions();

        private SREnemyStatsOptions enemyStatsOptions = new SREnemyStatsOptions();

        public override void LateSetup()
        {
            Debug.developerConsoleVisible = false;
            SRDebug.Instance.AddOptionContainer(playerStatsOptions);
            playerStatsOptions.Initialize();

            SRDebug.Instance.AddOptionContainer(enemyStatsOptions);
            enemyStatsOptions.Initialize(_basicEnemyStats, _bullStats, _straferStats, _mosquitoStats);
        }
    }
}
