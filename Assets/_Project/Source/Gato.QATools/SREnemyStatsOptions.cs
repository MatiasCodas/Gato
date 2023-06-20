using Gato.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Gato.QATools
{
    public class SREnemyStatsOptions
    {
        private BasicEnemyStats _basicEnemyStats;
        private BullStats _bullStats;
        private StraferStats _straferStats;
        private StraferStats _mosquitoStats;

        public void Initialize(BasicEnemyStats basicEnemyStats, BullStats bullStats, StraferStats straferStats, StraferStats mosquitoStats)
        {
            _basicEnemyStats = basicEnemyStats;
            _bullStats = bullStats;
            _straferStats = straferStats;
            _mosquitoStats = mosquitoStats;
        }

        [Category("BasicEnemyStats")]
        public float MovementSpeed
        {
            get => _basicEnemyStats.Speed;
            set => _basicEnemyStats.Speed = value;
        }

        [Category("BasicEnemyStats")]
        public float TimeToDie
        {
            get => _basicEnemyStats.TimeToDie;
            set => _basicEnemyStats.TimeToDie = value;
        }

        [Category("BullStats")]
        public float DashSpeed
        {
            get => _bullStats.DashSpeed;
            set => _bullStats.DashSpeed = value;
        }

        [Category("BullStats")]
        public float TelegraphTime
        {
            get => _bullStats.TelegraphTime;
            set => _bullStats.TelegraphTime = value;
        }

        [Category("BullStats")]
        public float DistanceToAggro
        {
            get => _bullStats.DistanceToAggro;
            set => _bullStats.DistanceToAggro = value;
        }

        [Category("BullStats")]
        public float ChargeTime
        {
            get => _bullStats.ChargeTime;
            set => _bullStats.ChargeTime = value;
        }
         
        [Category("BullStats")]
        public float RestTime
        {
            get => _bullStats.RestTime;
            set => _bullStats.RestTime = value;
        }

        [Category("StraferStats")]
        public float StraferMovementVariation
        {
            get => _straferStats.MovementVariation;
            set => _straferStats.MovementVariation = value;
        }

        [Category("StraferStats")]
        public float StraferShakeStrength
        {
            get => _straferStats.ShakeStrength;
            set => _straferStats.ShakeStrength = value;
        }

        [Category("MosquitoStats")]
        public float MosquitoMovementVariation
        {
            get => _mosquitoStats.MovementVariation;
            set => _mosquitoStats.MovementVariation = value;
        }

        [Category("MosquitoStats")]
        public float MosquitoShakeStrength
        {
            get => _mosquitoStats.ShakeStrength;
            set => _mosquitoStats.ShakeStrength = value;
        }
    }
}
