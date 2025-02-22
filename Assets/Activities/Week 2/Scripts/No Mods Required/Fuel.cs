using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwo
{
    public class Fuel : MonoBehaviour
    {
        [SerializeField] private int fuelValue = 5; // the amount of fuel that we are given when this is picked up

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.GetComponent<Lander>())
            {
                // invoke the event that the player picked something up
                LunarLanderGameManager.playerPickedUp?.Invoke();

                // increase the current fuel by the fuel value
                collision.transform.root.GetComponent<Lander>().CurrentFuel += fuelValue;

                // destroy this collectable
                Destroy(gameObject);
            }
        }
    }
}
