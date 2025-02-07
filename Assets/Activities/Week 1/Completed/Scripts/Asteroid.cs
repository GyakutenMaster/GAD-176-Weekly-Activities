using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekOne.Completed
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float damage = 10;
        private Transform bodyToOrbit;
        private float orbitRadius = 5f;
        [SerializeField] private float orbitSpeed = 2f;
        private float orbitAngle = 0;

        private void Update()
        {
            UpdatePostion();
        }

        void UpdatePostion()
        {
            // Use trigonometric functions to create circular orbit
            // our orbit angle will be our delta time multiplied by our ourbit speed
            orbitAngle += Time.deltaTime * orbitSpeed;
            // our X will be caldulated using Cos of the orbit angle, multiplied by our orbit radius.
            float x = Mathf.Cos(orbitAngle) * orbitRadius;
            // our y will be caldulated using Sin of the orbit angle, multiplied by our orbit radius.
            float y = Mathf.Sin(orbitAngle) * orbitRadius;

            // Update the position of the celestial body
            transform.position = bodyToOrbit.position + new Vector3(x, y, 0f);
        }

        public void InitialiseAsteroid(float Angle, Transform OrbitObject, float OrbitRadius)
        {
            orbitAngle = Angle;
            bodyToOrbit = OrbitObject;
            orbitRadius = OrbitRadius;
        }

        private void OnCollisionEnter(Collision collision)
        {  
            // check to see if the object hit by the asteroid has a player script.
            if (collision.transform.GetComponent<Player>())
            {
                // if it does change the players health using the ChangeHealth function and passing
                // in our damage, remember we'll need to pass in the damage as a negative number.
                collision.transform.GetComponent<Player>().ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}