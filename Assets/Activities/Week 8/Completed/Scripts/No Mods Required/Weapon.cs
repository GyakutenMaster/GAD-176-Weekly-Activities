using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEight.Completed
{
    [CreateAssetMenu(fileName = "Base Weapon Week 8", menuName = "Completed/Week 8/Weapons/Weapon Base", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] protected string weaponName;
        [SerializeField] protected GameObject weaponModel;
        protected Transform firePoint;
        [SerializeField] protected int damage;
        [SerializeField] protected float range = 10;
        [SerializeField] protected int attackSpeed;
        [SerializeField] protected int ammo;
        [SerializeField] protected int clipSize;
        [SerializeField] protected int currentAmmo;
        [SerializeField] protected bool hasUnlimitedAmmo;
        protected GameObject weaponInstance = null;
        [SerializeField] protected Vector3 weaponSpawnOffSet;
        

        protected virtual void Start()
        {
            currentAmmo = clipSize; 
        }

        public string WeaponName
        {
            get { return weaponName; }
        }

        public virtual void Equip(bool active, Transform weaponSpawnPoint = null)
        {
            if (!weaponModel)
            {
                return;
            }

            if(weaponSpawnPoint != null)
            {
                if (weaponInstance == null)
                {
                    weaponInstance = Instantiate(weaponModel, weaponSpawnPoint);
                    Transform firePointFound = weaponInstance.transform.Find("FirePoint");

                    weaponInstance.transform.localPosition += weaponSpawnOffSet;

                    if(firePointFound != null )
                    {
                        firePoint = firePointFound;
                    }
                }
            }

            weaponInstance.SetActive(active);
        }

        public virtual void Fire()
        {
            if (currentAmmo > 0 || hasUnlimitedAmmo)
            {
                ShootBullet();
                currentAmmo--;
            }
            else
            {
                Debug.Log("Out of ammo! Reload before firing.");
            }
        }

        public virtual void SecondaryFunction()
        {
            Debug.Log($"{weaponName} performs secondary function.");
        }

        public virtual void Reload()
        {
            if (currentAmmo < clipSize && ammo > 0)
            {
                int ammoToReload = Mathf.Min(clipSize - currentAmmo, ammo);
                currentAmmo += ammoToReload;
                ammo -= ammoToReload;

                Debug.Log($"Reloaded {ammoToReload} bullets. Remaining ammo: {currentAmmo}");
            }
            else if (ammo > 0)
            {
                Debug.Log("Clip is already full No need to reload.");
            }
            else
            {
                Debug.Log("We ran out of ammo");
            }
        }

        protected virtual void ShootBullet()
        {
            if (firePoint != null)
            {
                RaycastHit hit;

                // example if you want to shoot from the gun fire point.
                //Debug.DrawRay(firePoint.position, firePoint.forward, Color.red, 5);

                //if(Physics.Raycast(firePoint.position, firePoint.forward, out hit))
                //{
                //    if(hit.transform != null)
                //    {
                //        if(hit.transform.GetComponent<Enemy>())
                //        {
                //            hit.transform.GetComponent<Enemy>().ChangeHealth(-damage);
                //        }
                //    }
                //}

                // shoot this weapon from the middle of the screen.
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, 5);

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.GetComponent<Enemy>())
                        {
                            hit.transform.GetComponent<Enemy>().ChangeHealth(-damage);
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