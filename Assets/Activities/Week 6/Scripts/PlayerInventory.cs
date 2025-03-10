using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekSix
{
    public class PlayerInventory : MonoBehaviour
    {
        protected Weapon currentEquipedWeapon;
        [SerializeField] protected List<Weapon> allWeaponScriptableObjects = new List<Weapon>();
        protected List<Weapon> allWeaponInstances = new List<Weapon>();

        [SerializeField] protected Transform weaponSpawnPoint;

        public Weapon CurrentWeapon
        {
            get
            {
                return currentEquipedWeapon;
            }
            set
            {
                if (currentEquipedWeapon)
                {
                    currentEquipedWeapon.Equip(false);
                }
                currentEquipedWeapon = value;
                currentEquipedWeapon.Equip(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SpawnWeapons();
            UnequipAllWeapons();
            CurrentWeapon = allWeaponInstances[0];
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        void SpawnWeapons()
        {
            for (int i = 0; i < allWeaponScriptableObjects.Count; i++)
            {
                // so here let's spawn an instance of the scriptable object we are up to, by using ScriptableObject.Instantiate.
                Weapon instanceWeapon = ScriptableObject.Instantiate(allWeaponScriptableObjects[i]);

                if (instanceWeapon != null)
                {
                    // lets add our new weapon to our allWeaponsInstances List.
                    // lets then access the equip function of our instanced weapon and pass in true and the weapon spawn point.
                    allWeaponInstances.Add(instanceWeapon);
                    instanceWeapon.Equip(true, weaponSpawnPoint);
                }    
            }
        }

        void UnequipAllWeapons()
        {
            for (int i = 0; i < allWeaponInstances.Count; i++)
            {
                allWeaponInstances[i].Equip(false);
            }
        }

        void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurrentWeapon.Fire();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CurrentWeapon.SecondaryFunction();
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                CurrentWeapon = allWeaponInstances[GetNextWeapon(1)];
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                CurrentWeapon = allWeaponInstances[GetNextWeapon(-1)];
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                CurrentWeapon.Reload();
            }
        }

        private int GetNextWeapon(int iteration)
        {
            int current = GetEquippedWeaponIndex() + iteration;

            if (current >= allWeaponInstances.Count)
            {
                current = 0;
            }
            else if (current < 0)
            {
                current = allWeaponInstances.Count - 1;
            }

            return current;
        }

        private int GetEquippedWeaponIndex()
        {
            for (int i = 0; i < allWeaponInstances.Count; i++)
            {
                if (currentEquipedWeapon == allWeaponInstances[i])
                {
                    return i;
                }
            }
            // we couldnt find the weapon in the list
            Debug.Log("Weapon not found, returning 0");
            return 0;
        }
    }
}
