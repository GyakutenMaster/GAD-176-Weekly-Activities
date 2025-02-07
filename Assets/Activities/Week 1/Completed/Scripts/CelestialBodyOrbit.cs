using System.Collections;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekOne.Completed
{
    public class CelestialBodyOrbit : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private int numberOfAsteroids = 4;
        [SerializeField] private float orbitRadius = 5f;
        [SerializeField] private float asteroidOffset = 1f;
        [SerializeField] private float orbitGizmoResolution = 36;
        private const float degreesInCircle = 360;

        private void Start()
        {
            SpawnAsteroids();
        }

        void SpawnAsteroids()
        {
            // here let's loop over the number of asteroids and spawn one in.
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                // to make it cool, we are going to spawn it in at an angle, to achieve this we want to pass
                // into our calculate asteroid angle function the current iteration we are up to.
                float angle = CalculateAsteroidSpawnAngle(i);
                // we then want to call our calculate orbit position and pass in the angle.
                Vector3 asteroidPosition = CalculateOrbitPosition(angle);
                // we are going to spawn in the asteroid at the position.
                GameObject clone = Instantiate(asteroidPrefab, asteroidPosition, Quaternion.identity);
                // here we are going to access the Asteroid script and pass in the angle, the transform of the planet and the orbit radius.
                clone.GetComponent<Asteroid>().InitialiseAsteroid(angle, transform, orbitRadius);
            }
        }

        float CalculateAsteroidSpawnAngle(int index)
        {
            // our angle increment will be the degrees in a circle divided by the number of asteroids.
            float angleIncrement = degreesInCircle / numberOfAsteroids;
            // our spawn angle will be the current index we are up to, multiplied by the angle increment
            // plus our asteroid offset multiplied by our index.
            float spawnAngle = index * angleIncrement + asteroidOffset * index;
            return spawnAngle + asteroidOffset;
        }

        Vector3 CalculateOrbitPosition(float angle)
        {
            // our x will be caculated using Cos and pasing in our degree 2 radians multiplied by the angle, and then multiplying the result by our orbit radius.
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * orbitRadius;
            // our x will be caculated using Sin and pasing in our degree 2 radians multiplied by the angle, and then multiplying the result by our orbit radius.
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * orbitRadius;
            return new Vector3(x, y, 0f) + transform.position;
        }

        void OnDrawGizmos()
        {
            DrawAsteroidOrbits();
            DrawAsteroidSpawnLocations();
        }

        void DrawAsteroidOrbits()
        {
            Gizmos.color = Color.red;

            // Draw Gizmos for orbit trajectory
            // we are going to loop over our orbitGizmo Resolution number
            for (int i = 0; i < orbitGizmoResolution; i++)
            {
                // our angle will be the current iteraction we are up to, multiplied by our degrees in a circle divided by our orbitGizmo resolution don't forget ()
                float angle = i * (degreesInCircle / orbitGizmoResolution);
                Vector3 worldPosition = CalculateOrbitPosition(angle);
                Gizmos.DrawSphere(worldPosition, 0.1f);

                // Draw a line connecting each Gizmo point for better visualization
                // we then want to check to see if the current iteration is greater than 0
                if (i > 0)
                {
                    // our previous world position will be our
                    // Calculate our previous angle by first taking 1 away from the current iteration to get last iteration
                    // then multiplying this by the degrees in a circle divided by our orbit gizmo resolution ( ) will be your friend.
                    Vector3 previousWorldPosition = CalculateOrbitPosition((i - 1) * (degreesInCircle / orbitGizmoResolution));
                    Gizmos.DrawLine(previousWorldPosition, worldPosition);
                }
            }
        }

        void DrawAsteroidSpawnLocations()
        {
            Gizmos.color = Color.blue;
            // here we are going to loop over the number of asteroids.
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                // the angle will be using our spawn angle and passing in the current iteration we are up to.
                float angle = CalculateAsteroidSpawnAngle(i);
                // we'll then need to calculate the asteroid position using our CalculateOrbitPosition and passing in the angle.
                Vector3 asteroidPosition = CalculateOrbitPosition(angle);
                // we then draw the sphere for our spawn location.
                Gizmos.DrawWireSphere(asteroidPosition, 0.5f);
            }
        }
    }
}
