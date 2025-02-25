using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree
{
    public class Pistol : Weapon
    {
        public override void Fire()
        {
            // here let's call the base fire function.
            base.Fire();
            Debug.Log("Pistol fires semi-automatically.");
        }

        public override void SecondaryFunction()
        {
            // here let's call the secondary function in the base class.
            // then let's call the Reload function also in the base.
            base.SecondaryFunction();
            Reload();
        }
    }
}
