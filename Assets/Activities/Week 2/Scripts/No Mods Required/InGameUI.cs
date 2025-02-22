using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekTwo
{
    [System.Serializable]
    public class InGameUI
    {
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private Text fuelGaugeText;
        [SerializeField] private Slider fuelGauge; // reference to our fuel gauge
        [SerializeField] private Slider velocityGauge; // reference to our velocity gauge
        private LunarLanderUIManager uiManager;

        public void SetUp(LunarLanderUIManager current)
        {
            uiManager = current;
            ShowInGameUI(true);
        }

        /// <summary>
        /// Updates the fuel gauge with my new fuel value
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateFuel(float amount)
        {
            fuelGauge.value = amount;
        }

        /// <summary>
        /// Updates the ui with my new normalised velocity
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateVelocity(float amount)
        {
            velocityGauge.value = amount;
        }

        public void ShowInGameUI(bool Enable)
        {
            inGameUI.SetActive(Enable);
        }
    }
}
