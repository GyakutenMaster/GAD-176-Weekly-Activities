using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekThree
{
    public class PlayerInventory : MonoBehaviour
    {
        private Weapon currentEquipedWeapon;
        [SerializeField] private List<Weapon> allWeapons = new List<Weapon>();

        public Weapon CurrentWeapon
        {
            get
            {
                return currentEquipedWeapon;
            }
            set
            {
                // so here lets check if there is a currentWeapon.
                // if there is let's first unequip it using the Equip function of the Weapon Class.
                
                currentEquipedWeapon = value;
                // now we've set the new weapon
                // let's make sure we call the equip function of the weapon class.
                
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            UnequipAllWeapons();
            CurrentWeapon = allWeapons[0];
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        void UnequipAllWeapons()
        {
            for (int i = 0; i < allWeapons.Count; i++)
            {
                // here let's access the current weapon iteration
                // and call the Equip function of it, but let's hide it rather than showing it.
            }
        }

        void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // lets access the current weapon and call the fire function
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // lets access the current weapon and call the secondary fire function
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                CurrentWeapon = allWeapons[GetNextWeapon(1)];
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                CurrentWeapon = allWeapons[GetNextWeapon(-1)];
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                 // lets access the current weapon and call the reload function
            }
        }

        private int GetNextWeapon(int iteration)
        {
            int current = GetEquippedWeaponIndex() + iteration;

            if (current >= allWeapons.Count)
            {
                current = 0;
            }
            else if (current < 0)
            {
                current = allWeapons.Count - 1;
            }

            return current;
        }

        private int GetEquippedWeaponIndex()
        {
            for (int i = 0; i < allWeapons.Count; i++)
            {
                if (currentEquipedWeapon == allWeapons[i])
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
