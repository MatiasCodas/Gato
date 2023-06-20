using UnityEngine;
using Gato.Gameplay;
using Gato.Core;
using SRDebugger;
using System.ComponentModel;

namespace Gato.QATools
{
    public class SRPlayerStatsOptions
    {
        private PlayerStats _playerStats;

        internal void Initialize()
        {
            IPlayerControlService playerControlSystem = ServiceLocator.Shared.Get<IPlayerControlService>();
            _playerStats = playerControlSystem.FetchPlayerStats();
        }

        [Category("PlayerStats")]
        public float MovementSpeed
        {
            get => _playerStats.MovementSpeed;
            set => _playerStats.MovementSpeed = value;
        }

        [Category("PlayerStats")]
        public float DashSpeed
        {
            get => _playerStats.DashSpeed;
            set => _playerStats.DashSpeed = value;
        }

        [Category("PlayerStats")]
        public float DashCooldown
        {
            get => _playerStats.DashCooldown;
            set => _playerStats.DashCooldown = value;
        }

        [Category("PlayerStats")]
        public float DashTime
        {
            get => _playerStats.DashTime;
            set => _playerStats.DashTime = value;
        }

        [Category("PlayerStats")]
        public float RopeTime
        {
            get => _playerStats.RopeTime;
            set => _playerStats.RopeTime = value;
        }

        [Category("PlayerStats")]
        public float RopeSize
        {
            get => _playerStats.RopeSize;
            set => _playerStats.RopeSize = value;
        }

        [Category("PlayerStats")]
        public float RopeCooldown
        {
            get => _playerStats.RopeCooldown;
            set => _playerStats.RopeCooldown = value;
        }

        [Category("PlayerStats")]
        public float ProjectileSpeed
        {
            get => _playerStats.ProjectileSpeed;
            set => _playerStats.ProjectileSpeed = value;
        }
    }
}
