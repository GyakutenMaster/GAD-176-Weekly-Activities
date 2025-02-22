using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree
{
    public class Shotgun : Weapon
    {
        [SerializeField] protected int pelletCount;
        [SerializeField] protected float spreadAngle = 30f; // Adjust the spread angle as needed

        public override void Fire()
        {
            // here let's calcuate the angle increment
            // this will be our spreadAngle divided by our pelletCount - 1
            // parenthesis will be your friend. we use the -1 here to take into account we'll be starting at 0.
            float angleIncrement = 0;

            // here let's loop over the pellet count
            for (int i = 0; i < pelletCount; i++)
            {
                float pelletAngle = -spreadAngle / 2 + i * angleIncrement;
                FireSinglePellet(pelletAngle);
            }
        }

        private void FireSinglePellet(float angle)
        {
            Debug.Log($"Shotgun fires a single pellet at angle {angle} degrees.");


            // here let's spawn in our bulletPrefab at the firepoint position
            // then for the rotation using the Quarternion.Euler function pass in the angle for the Z axis, the other can be 0. 
            GameObject pellet = null;
            Destroy(pellet, 5);
        }

        public override void SecondaryFunction()
        {
            // lets call the secondary function here from the base
            // then let's also call the reload function as well.
        }
    }
}
