using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GAD176.WeeklyActivities.WeekEight.Completed
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI itemName;
        [SerializeField] protected Image itemImage;
        [SerializeField] protected TextMeshProUGUI itemCost;
        [SerializeField] protected Button itemBuyButton;

        public void SetUp(ShopItem item, PlayerInventory inventory)
        {
            itemName.text = item.ItemName;
            itemImage.sprite = item.ItemSprite;
            itemCost.text = "$" + item.Cost.ToString();
            itemBuyButton.onClick.RemoveAllListeners();
            itemBuyButton.onClick.AddListener(() => { BuyItem(item, inventory); });
        }

        private void BuyItem(ShopItem item, PlayerInventory inventory)
        {
            if (inventory && inventory.CanBuyItem(item.Cost))
            {   
                if (!inventory.WeaponAlreadyInInventory(item.WeaponScriptableObject as Weapon))
                {
                    // access the change wallet amount from the wallet, and pass in the item cost.
                    inventory.ChangeWalletAmount(-item.Cost);
                    // here lets use the Instantiate of the scriptable object class pass in the scriptable object from the item.
                    // the last step is you'll need to cast the scriptable object as a weapon, we did this back in 
                    // gad170 with ints and floats () same deal here but it will be Weapon.
                    Weapon itemInstance = (Weapon)ScriptableObject.Instantiate(item.WeaponScriptableObject);
                    // lastley access the AddNewWeapon from the inventory class and pass in the item instance.
                    inventory.AddNewWeapon(itemInstance);
                }
                else
                {
                    Debug.Log("Weapon already in inventory");
                }
            }
            else
            {
                Debug.Log("Not enough monies.");
            }
        }
    }
}
