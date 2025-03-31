using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEight
{
    [CreateAssetMenu(fileName = "Melee Week 8", menuName = "Week 8/Weapons/Melee Weapon", order = 0)]
    public class Knife : Weapon
    {
        public override void Fire()
        {
            base.Fire();
            Debug.Log($"Knife slashes with a speed of {attackSpeed} attacks per second.");
        }

        public override void SecondaryFunction()
        {
            Debug.Log($"Knife performs a quick stabbing motion.");
            if (firePoint != null)
            {
                RaycastHit hit;
                // shoot this weapon from the middle of the screen.
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, 5);

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
                {
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