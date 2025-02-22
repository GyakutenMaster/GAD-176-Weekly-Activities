using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
{
    public class BurstShot : Weapon
    {
        // You can add specific properties or methods for BurstShot if needed
        [SerializeField] private int burstShotCount = 3; // Number of shots in each burst
        private Coroutine firingRoutine;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start(); // Call the base Start method
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Override the Fire method for burst firing
        public override void Fire()
        {
            if (CanFire() && firingRoutine == null)
            {
                // here lets start call startcoroutine and start the BurstFire routine, and lets store it in the firing routine.
                firingRoutine = null;
            }
        }

        // Implement burst firing logic
        private IEnumerator BurstFire()
        {
            // here lets set the next fire time to be the current time.time plus the firerate.
            nextFireTime = 0;
            
            
            // lets loop for the number of burst fire bullets.
            for (int i = 0; i < 1; i++)
            {
                ShootBullet();
                // here lets yield return new wait for seconds and lets wait the length of the firerate.
                yield return null;
            }
            firingRoutine = null;
        }
    }
}
