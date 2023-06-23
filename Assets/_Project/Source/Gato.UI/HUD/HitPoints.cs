using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Gato.UI
{
    public class HitPoints : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hitPointsText;

        public static Action<int, int> OnIncreaseHitPoints;

        private void Awake()
        {
            OnIncreaseHitPoints += IncreaseHitPointsText;
        }

        private void OnDestroy()
        {
            OnIncreaseHitPoints -= IncreaseHitPointsText;
        }

        private void IncreaseHitPointsText(int _currentHP, int _maxHP)
        {
            _hitPointsText.text = "HP: " + _currentHP.ToString() + "/" + _maxHP.ToString();
        }
    }
}
