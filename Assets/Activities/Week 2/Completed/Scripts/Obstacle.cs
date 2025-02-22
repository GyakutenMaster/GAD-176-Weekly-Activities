using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwo.Completed
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected float speed = 5f;

        private void Update()
        {
            Move();
        }

        // Common behavior for all obstacles
        protected void Move()
        {
            // lets move our obstacle by vector3.down
            // multiplied by our speed and our delta time.
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}