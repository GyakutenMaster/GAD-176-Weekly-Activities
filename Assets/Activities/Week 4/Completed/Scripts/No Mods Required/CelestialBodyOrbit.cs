using System.Collections;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour.Completed
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
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                float angle = CalculateAsteroidSpawnAngle(i);
                Vector3 asteroidPosition = CalculateOrbitPosition(angle);

                GameObject clone = Instantiate(asteroidPrefab, asteroidPosition, Quaternion.identity);
                clone.GetComponent<Asteroid>().InitialiseAsteroid(angle, transform, orbitRadius);
            }
        }

        float CalculateAsteroidSpawnAngle(int index)
        {
            float angleIncrement = degreesInCircle / numberOfAsteroids;
            float spawnAngle = index * angleIncrement + asteroidOffset * index;
            return spawnAngle + asteroidOffset;
        }

        Vector3 CalculateOrbitPosition(float angle)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * orbitRadius;
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
            for (int i = 0; i < orbitGizmoResolution; i++)
            {
                float angle = i * (degreesInCircle / orbitGizmoResolution);
                Vector3 worldPosition = CalculateOrbitPosition(angle);
                Gizmos.DrawSphere(worldPosition, 0.1f);

                // Draw a line connecting each Gizmo point for better visualization
                if (i > 0)
                {
                    Vector3 previousWorldPosition = CalculateOrbitPosition((i - 1) * (degreesInCircle / orbitGizmoResolution));
                    Gizmos.DrawLine(previousWorldPosition, worldPosition);
                }
            }
        }

        void DrawAsteroidSpawnLocations()
        {
            Gizmos.color = Color.blue;

            for (int i = 0; i < numberOfAsteroids; i++)
            {
                float angle = CalculateAsteroidSpawnAngle(i);
                Vector3 asteroidPosition = CalculateOrbitPosition(angle);
                Gizmos.DrawWireSphere(asteroidPosition, 0.5f);
            }
        }
    }
}
