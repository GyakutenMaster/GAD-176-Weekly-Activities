using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree
{
    public class MachineGun : Weapon
    {
        [SerializeField] private int BurstShotCount = 3;
        [SerializeField] private float BurstFireRate = 10f; // Shots per second
        private bool isBurstFire = false;
        private bool isBurstFiring = false;

        public void SwitchToSingleShot()
        {
            isBurstFire = true;
        }

        public override void Fire()
        {
            if (isBurstFire && !isBurstFiring)
            {
                StartCoroutine(BurstFire());
            }
            else if (!isBurstFire)
            {
                FireSingleShot();
            }
        }

        public override void SecondaryFunction()
        {
            // here let's call the base scripts secondary function

            // then let's call the switch fire mode function.
        }

        private void SwitchFireMode()
        {
            Debug.Log($"Submachine gun switches to burst fire mode.");
            // here we want to check if it's already burst fire mode, if it is let's sawp it to not being 
            // burst fire, we just need to change the bool's value.
        }

        private void FireSingleShot()
        {
            Debug.Log($"Submachine gun fires a single shot.");
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(bullet, 5);
        }

        private IEnumerator BurstFire()
        {
            isBurstFiring = true;

            for (int i = 0; i < BurstShotCount; i++)
            {
                FireSingleShot();
                // here' lets use a new wait for seconds and the amount time should be 1 divided by the burst fire rate.
                // this is effectively 1 second divide by how many bullets should fire.
                yield return null;
            }

            isBurstFiring = false;
        }
    }
}
