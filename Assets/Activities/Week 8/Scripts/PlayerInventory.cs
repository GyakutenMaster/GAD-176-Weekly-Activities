using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GAD176.WeeklyActivities.WeekEight
{
    public class PlayerInventory : MonoBehaviour
    {
        protected Weapon currentEquipedWeapon;
        [SerializeField] protected Weapon defaultWeapon;
        [SerializeField] protected List<Weapon> allWeaponInstances = new List<Weapon>();

        [SerializeField] protected Transform weaponSpawnPoint;
        [SerializeField] protected ShopUI shopUI;
        [SerializeField] protected int wallet = 200;

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
                if(currentEquipedWeapon != null)
                {
                    currentEquipedWeapon.Equip(true);
                }      
            }
        }

        private void Start()
        {
            AddNewWeapon(ScriptableObject.Instantiate(defaultWeapon));
            shopUI = FindObjectOfType<ShopUI>();
            ChangeWalletAmount(200);
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        public void AddNewWeapon(Weapon weaponInstance)
        {
            // here lets add the weapon isntance coming in to our all weapon instances.
            allWeaponInstances.Add(weaponInstance);
            // then lets access the equip function of the weapon instance and we want it active
            // and we want it to be parented to the weapon spawn point.
            weaponInstance.Equip(true, weaponSpawnPoint);
            // finally lets set the CurrentWeapon to be the weapon instance.
            CurrentWeapon = weaponInstance;
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
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(shopUI != null)
                {
                    // here lets access the Shop Ui and call the toggle shop function
                    shopUI.ToggleShop();
                }
            }
            if (shopUI != null && !shopUI.IsShopOpen())
            {
                if (Input.GetMouseButtonDown(0) && CurrentWeapon)
                {
                    CurrentWeapon.Fire();
                }
                else if (Input.GetMouseButtonDown(1) & CurrentWeapon)
                {
                    CurrentWeapon.SecondaryFunction();
                }
                else if (Input.mouseScrollDelta.y > 0)
                {
                    if (allWeaponInstances.Count > 0)
                    {
                        CurrentWeapon = allWeaponInstances[GetNextWeapon(1)];
                    }
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    if (allWeaponInstances.Count > 0)
                    {
                        CurrentWeapon = allWeaponInstances[GetNextWeapon(-1)];
                    }
                }
                else if (Input.GetKeyDown(KeyCode.R) && CurrentWeapon)
                {
                    CurrentWeapon.Reload();
                }
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

        public bool WeaponAlreadyInInventory(Weapon weapon)
        {
            bool weaponExists = false;

            // here lets loop all the weapons instances
            // then lets compare the weapon coming in's name
            // to the current weapon instance we are up to in the loop.
            // finally if it the current instance and the instances name coming in's name matches
            // then set the weaponExists to true.
            for (int i = 0; i < allWeaponInstances.Count; i++)
            {
                if (weapon.WeaponName == allWeaponInstances[i].WeaponName)
                {
                    weaponExists = true;
                }
            }
            return weaponExists;
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

        public void ChangeWalletAmount(int amount)
        {
            // here lets change the wallet amount by the amount coming into the function, we use this when buying.
            wallet += amount;
            if (shopUI != null)
            {
                // lets call the Update Wallet function and pass in the current value of the wallet.
                shopUI.UpdateWallet(wallet);
            }
        }

        public bool CanBuyItem(int itemCost)
        {
            // here lets return true if the wallet amount minus the item cost is greater or equal to 0
            // this would mean we have enough money to buy the item.
            return wallet - itemCost >= 0;
        }
    }
}
