using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree.Completed
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected string weaponName;
        [SerializeField] protected GameObject weaponModel;
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected int damage;
        [SerializeField] protected int attackSpeed;
        [SerializeField] protected int ammo;
        [SerializeField] protected int clipSize;
        [SerializeField] protected int currentAmmo;

        protected virtual void Start()
        {
            currentAmmo = clipSize;
        }

        public virtual void Equip(bool active)
        {
            if (!weaponModel)
            {
                return;
            }
            // here if the active bool coming in is true or false
            // set the weapon model to be active or not we can use setActive for that.
            weaponModel.SetActive(active);
        }

        public virtual void Fire()
        {
            if (currentAmmo > 0)
            {
                // here let's call the shoot bullet function
                // lets also take the ammo away from the currentAmmo.
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
                // here lets grab how much ammo needs to be reloaded i.e. if a clip 
                // is 10 bullets and I've shot 3 I should only reload the 3.
                // to do this let's use Mathf Min and we can pass in the ammo
                // and the other variable should be the value of the clipSize minus the currentAmmo.
                int ammoToReload = Mathf.Min(clipSize - currentAmmo, ammo);
                // here lets add onto the current ammo the amount to reload.
                currentAmmo += ammoToReload;
                // then lets take the amount to reload away from the ammo.
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
            // you could investigate in using this function in classes that inherit from this.
            GameObject clone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(clone, 5);
        }
    }
}