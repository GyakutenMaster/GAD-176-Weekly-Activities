using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GAD176.WeeklyActivities.WeekTwo.Completed
{
    public class LandingPad : MonoBehaviour
    {
        [SerializeField] private float timeTillSceneLoad = 3;
        private bool IsLoadingLevel = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // only load the level if its the lander hitting this, and it's not falling too fast
            if (collision.transform.root.GetComponent<Lander>() && !collision.transform.root.GetComponent<Lander>().IsFallingTooFast())
            {
                Invoke("LoadNextLevel", timeTillSceneLoad);
            }
        }

        private void LoadNextLevel()
        {
            if (IsLoadingLevel)
            {
                return;
            }
            Debug.Log("Player landed, you win");
            Debug.Log("Load next level");
            IsLoadingLevel = true;
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                // if we land, load the next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("Game has been won woo!");
            }
        }
    }
}