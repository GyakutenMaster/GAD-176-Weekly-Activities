using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekSix.Completed
{
    [CreateAssetMenu(fileName = "Grenade", menuName = "Completed/Week 6/Weapons/Grenade", order = 0)]
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
                // so our throw vector should be the forward direction of the firepoint plus a vector3.up
                Vector3 throwVector = firePoint.forward + Vector3.up;
                // here lets multiply the throw vector by thr range of our weapon.
                rigid.AddForce(throwVector * range);
            }

            if (explosion)
            {
                explosion.Initiate();
            }
        }
    }
}
