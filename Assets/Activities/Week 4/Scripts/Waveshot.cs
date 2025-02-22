using System.Collections;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
{
    public class Waveshot : Weapon
    {
        [SerializeField] private float frequency = 2f; // Adjust the frequency of the sine wave
        [SerializeField] private float amplitude = 1f; // Adjust the amplitude of the sine wave
        [SerializeField] private int numberOfShots = 5; // Number of shots to fire
        [SerializeField] private float timeBetweenShots = 0.1f; // Time between each shot
        [SerializeField] private float verticalOffset = 0.1f; // Vertical offset for each bullet

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start(); // Call the base Start method
        }

        // Override the Fire method to initiate bullet firing
        public override void Fire()
        {
            StartCoroutine(FireBullets());
        }

        private IEnumerator FireBullets()
        {
            // lets loop through the number of shots.
            for (int i = 0; i < 1; i++)
            {

                // our time will be our current Time.time multiplied by the frequency.
                // plus the current iteration we are up to, multiplied by the time between shots.
                // this determines the x axis pos.
                float time = 0; 

                // our y offset will be our amplitude multiplied by Sin of the time.
                float yOffset =0;

                // Our x axis will be our amplidue multiplied by sin of our time.
                float x = 0;
                // our y will be our yOffset plus our current iteration multiplied by our vertical offset.
                float y = 0;

                Vector3 offset = new Vector3(x, y, 0f);

                // this will be our firepoint position plus our offset.
                Vector3 firingPosition = Vector3.zero;

                // Instantiate the bullet at the adjusted position
                GameObject clone = Instantiate(bulletPrefab, firingPosition, firePoint.rotation);
                Destroy(clone, 5);

                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }
}
