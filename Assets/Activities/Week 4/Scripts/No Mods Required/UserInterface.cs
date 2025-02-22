using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekFour
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private Slider playerHealth;

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            playerHealth.value = currentHealth / maxHealth;
        }

        public void UpdateProgressSlider(float currentProgress, float maxProgress)
        {
            // Calculate the progress as a value between 0 and 1
            float progress = 1 - Mathf.Clamp01(currentProgress / maxProgress);
            // Debug.Log("Progress: " + progress);

            // Update the slider value based on the progress
            progressSlider.value = progress;
            // Debug.Log("Slider Value: " + slider.value);
        }
    }
}
