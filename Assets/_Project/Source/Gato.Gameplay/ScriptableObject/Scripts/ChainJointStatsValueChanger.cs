using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gato.UI
{
    public class ChainJointStatsValueChanger : MonoBehaviour
    {
        public TMP_Text UIValue;
        public Slider ShotSpeedSlider;
        public ChainJointStats ChainJointStats;
        private void Start()
        {
            ShotSpeedSlider.value = ChainJointStats.shotSpeed;
            UIValue.text = ChainJointStats.shotSpeed.ToString();
        }

        public void UpdateValue()
        {
            ChainJointStats.shotSpeed = ShotSpeedSlider.value;
            UIValue.text = ChainJointStats.shotSpeed.ToString();
        }
    }
}
