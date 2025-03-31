using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEight
{
    [CreateAssetMenu(fileName = "Grenade Week 8", menuName = "Week 8/Weapons/Grenade", order = 0)]
    public class Grenade : Weapon
    {
        protected override void ShootBullet()
        {
            // throw the grenade.
            // instantiate the grenade objects.
            GameObject clone = Instantiate(weaponModel, firePoint.position, firePoint.rotation);
            
            Debug.Log("Spawned?");

            Rigidbody rigid = clone.GetComponent<Rigidbody>();

            Explosion explosion = clone.GetComponent<Explosion>();

            if (rigid == null)
            {
                rigid = clone.AddComponent<Rigidbody>();
            }

            if(rigid != null)
            {
                rigid.AddForce((firePoint.forward + Vector3.up) * range);
            }

            if (explosion)
            {
                explosion.Initiate();
            }
        }
    }
}
