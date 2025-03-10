using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekSix
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Week 6/Weapons/MeleeWeapon", order = 0)]
    public class Knife : Weapon
    {
        public override void Fire()
        {
            // so here would could do some fancier logic, but instead let's keep it simple
            // and make the knife act like the gun but with super short range, so let's call our fire function in the base class.
            base.Fire();
            Debug.Log($"Knife slashes with a speed of {attackSpeed} attacks per second.");
        }

        public override void SecondaryFunction()
        {
            Debug.Log($"Knife performs a quick stabbing motion.");
            if (firePoint != null)
            {
                RaycastHit hit;

                Transform cameraTransform = Camera.main.transform;

                // so let's do a debug draw ray, it should take in the position of our camera, the forward direction of our camera.
                // a colour let's say Red, and it should last 5 seconds, just to confirm its firing in the right direction
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 5);

                // inside our if, let's do a physics.raycast it should take in camera position, camera forward, out hit, and the range of the gun. 
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range))
                {
                    // uncomment this section, once your physics.raycast is completed.

                    if (hit.transform != null)
                    {
                        if (hit.transform.GetComponent<Enemy>())
                        {
                            hit.transform.GetComponent<Enemy>().ChangeHealth(-damage / 2);
                        }
                    }
                }
                else
                {
                    Debug.Log("Hit Nothing");
                }
            }
        }
    }
}