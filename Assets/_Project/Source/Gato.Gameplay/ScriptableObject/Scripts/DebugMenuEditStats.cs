using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gato.UI
{
    public class DebugMenuEditStats : MonoBehaviour
    {
        [Header("Rope Variables")]
        public ChainJointStats ChainJointStats;
        public TMP_Text ShotSpeed;
        public Slider ShotSpeedSlider;
        public TMP_Text DistanceMultiplier;
        public Slider DistanceMultiplierSlider;

        [Header("Player Stats")]
        public PlayerStats PlayerStats;
        public TMP_Text MovementSpeed;
        public Slider MovementSpeedSlider;
        public TMP_Text DashSpeed;
        public Slider DashSpeedSlider;
        public TMP_Text DashCooldown;
        public Slider DashCooldownSlider;
        public TMP_Text DashTime;
        public Slider DashTimeSlider;
        public TMP_Text RopeTime;
        public Slider RopeTimeSlider;
        public TMP_Text RopeSize;
        public Slider RopeSizeSlider;
        public TMP_Text RopeCooldown;
        public Slider RopeCooldownSlider;

        private void Awake()
        {
            UpdateText();
        }

        public void UpdateValue()
        {
            
            UpdateSliderValue();
            UpdateText();

        }

        
        private void UpdateSliderValue()
        {
            //Rope Stats
            ChainJointStats.shotSpeed = ShotSpeedSlider.value;
            ChainJointStats.distanceMultiplier = DistanceMultiplierSlider.value;

            //Player Stats
            PlayerStats.MovementSpeed = MovementSpeedSlider.value;
            PlayerStats.DashSpeed = DashSpeedSlider.value;
            PlayerStats.DashCooldown = DashCooldownSlider.value;
            PlayerStats.DashTime = DashTimeSlider.value;
            PlayerStats.RopeTime = RopeTimeSlider.value;
            PlayerStats.RopeSize = RopeSizeSlider.value;
            PlayerStats.RopeCooldown = RopeCooldownSlider.value;
    }

        private void UpdateText()
        {
            //Rope Stats
            ShotSpeed.text = ChainJointStats.shotSpeed.ToString();
            DistanceMultiplier.text = ChainJointStats.distanceMultiplier.ToString();

            //Player Stats
            MovementSpeed.text = PlayerStats.MovementSpeed.ToString();
            DashSpeed.text = PlayerStats.DashSpeed.ToString();
            DashCooldown.text = PlayerStats.DashCooldown.ToString();
            DashTime.text = PlayerStats.DashTime.ToString();
            RopeTime.text = PlayerStats.RopeTime.ToString();
            RopeSize.text = PlayerStats.RopeSize.ToString();
            RopeCooldown.text = PlayerStats.RopeCooldown.ToString();
        }

    }
}
