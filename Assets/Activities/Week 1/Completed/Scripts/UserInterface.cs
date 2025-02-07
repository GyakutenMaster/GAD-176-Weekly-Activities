using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekOne.Completed
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private Slider playerHealth;

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            // let's set the vaalue to be our current health divided by our max health
            playerHealth.value = currentHealth / maxHealth;
        }

        public void UpdateProgressSlider(float currentProgress, float maxProgress)
        {
            // our progress will be a normalised value between 0 and 1
            // so we'll want to first take 1 (100 %) away from the current progress
            // imagine we are a quarter complted, on our slider we want to show 0.25.
            // so to achieve this we'll do 1 - our currentprogress divided by our max progress.
            // parenthesis will be your friend.
            float progress = 1 - Mathf.Clamp01(currentProgress / maxProgress);
            // Debug.Log("Progress: " + progress);

            // let's set the progress sliders value to be our progress.
            progressSlider.value = progress;
            // Debug.Log("Slider Value: " + slider.value);
        }
    }
}
