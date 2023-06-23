using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gato.Gameplay;
using System;

namespace Gato.UI
{
    public class HitPoints : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private TextMeshProUGUI _hitPointsText;

        private void Awake()
        {
            BasicEnemy.OnIncreaseHitPoints += IncreaseHitPointsText;
        }

        private void OnDestroy()
        {
            BasicEnemy.OnIncreaseHitPoints -= IncreaseHitPointsText;
        }

        private void IncreaseHitPointsText()
        {
            _hitPointsText.text = "HP: " + _playerStats.HitPoints.ToString() + "/" + _playerStats.MaxHP.ToString();
        }
    }
}
