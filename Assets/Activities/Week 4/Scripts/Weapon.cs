using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected GameObject weaponModel;
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected int damage;
        [SerializeField] protected int attackSpeed;
        protected float fireRate;
        protected float nextFireTime;

        protected virtual void Start()
        {
            // lets set the fire rate to be 1 divided by the attack speed.
            // this will be one second divided by the attack speed to get shots per second.
            fireRate = 0;
        }

        public virtual void Equip(bool active)
        {
            if (!weaponModel)
            {
                return;
            }
            weaponModel.SetActive(active);
        }

        public virtual void Fire()
        {
            if (CanFire())
            {
                // here lets call the shoot bullet function.

                // here lets set the next fire rate time to be the current time.time plus the firerate.
                nextFireTime = 0;
            }
        }

        protected virtual void ShootBullet()
        {
            GameObject clone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(clone, 5);
        }

        protected virtual bool CanFire()
        {
            // here lets check if the current Time.time is greater or equal to the next fire rate time.
            return false;
        }
    }
}

