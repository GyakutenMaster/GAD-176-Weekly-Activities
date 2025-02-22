using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree
{
    public class Knife : Weapon
    {
        public override void Fire()
        {
            Debug.Log($"Knife slashes with a speed of {attackSpeed} attacks per second.");
        }

        public override void SecondaryFunction()
        {
            Debug.Log($"Knife performs a quick stabbing motion.");
        }
    }
}