using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekSix
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float health = 100;

        public void ChangeHealth(float amount)
        {
            health += amount;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
