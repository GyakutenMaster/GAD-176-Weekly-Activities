using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwo
{
    public class Asteroid : Obstacle
    {
        [SerializeField] private float rotateSpeed = 30;
        private void Update()
        {
            MoveAsteroid();
        }

        // Unique behavior for asteroids
        private void Rotate()
        {
            // let's rotate our asteroid by our forward direction, 
            // by our rotate speed and our deltatime 
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }

        // Call Move and Rotate separately in an Asteroid-specific method
        protected void MoveAsteroid()
        {
            Move();
            Rotate();
        }
    }
}
