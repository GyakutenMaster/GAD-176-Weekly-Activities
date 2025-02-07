using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekOne.Completed
{
    public class EndZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // check to see if the other has a reference to the player script.
            if (other.GetComponent<Player>())
            {
                // if so do a hack here, and just set time scale to 0 to pause the game
                Time.timeScale = 0;
                Debug.Log("Level Completed, Load Next Level");
            }
        }
    }
}
