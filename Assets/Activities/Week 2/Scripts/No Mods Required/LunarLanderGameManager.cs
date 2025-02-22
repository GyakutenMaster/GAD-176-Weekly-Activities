using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace GAD176.WeeklyActivities.WeekTwo
{
    public class LunarLanderGameManager : MonoBehaviour
    {

        public static UnityEvent playerDeath = new UnityEvent();  // invoked when the player dies.
        public static UnityEvent playerApplyThrust = new UnityEvent();   // invoked when the player pressed arrow keys.
        public static UnityEvent playerPickedUp = new UnityEvent();   // invoked when the player picks up fuel.
    }
}