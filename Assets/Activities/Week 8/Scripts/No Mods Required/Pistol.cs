using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEight
{
    public class Pistol : Weapon
    {
        public override void Fire()
        {
            base.Fire();
            Debug.Log("Pistol fires semi-automatically.");
        }

        public override void SecondaryFunction()
        {
            base.SecondaryFunction();
            Reload();
        }
    }
}
