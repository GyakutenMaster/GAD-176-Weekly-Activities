using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
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
            orbitAngle += Time.deltaTime * orbitSpeed;
            float x = Mathf.Cos(orbitAngle) * orbitRadius;
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
            if (collision.transform.GetComponent<Player>())
            {
                collision.transform.GetComponent<Player>().ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}