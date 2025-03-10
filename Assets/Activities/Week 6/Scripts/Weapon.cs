using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekSix
{
    [CreateAssetMenu(fileName = "Base Weapon", menuName = "Week 6/Weapons/WeaponBase", order = 0)]
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
                    // here lets Insantiate the weapon model and parent it to the weapon spawn point.
                    weaponInstance = Instantiate(weaponModel, weaponSpawnPoint);

                    // here we are searching for the fire point child objet of the object we just spawned in.
                    Transform firePointFound = weaponInstance.transform.Find("FirePoint");

                    // lets access the weapon instances local position and add onto it the weaponSpawnOffset, this just allows us to adjust our guns spawn position.
                    weaponInstance.transform.localPosition += weaponSpawnOffSet;

                    if (firePointFound != null )
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