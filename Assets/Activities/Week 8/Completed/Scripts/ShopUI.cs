using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekEight.Completed
{
    public class ShopUI : MonoBehaviour
    {

        [SerializeField] protected List<ShopItem> items = new List<ShopItem>();
        [SerializeField] protected Transform shopItemParent;
        [SerializeField] protected GameObject shopItemPrefab;
        protected List<GameObject> allShopItemInstances = new List<GameObject>();
        [SerializeField] protected GameObject shopPannel;
        [SerializeField] protected Button closeButton;
        private PlayerInventory playerInventory;
        [SerializeField] protected TextMeshProUGUI walletValue;
        // Start is called before the first frame update
        void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => { shopPannel.SetActive(false); });
            CreateMenuItems();
            shopPannel.SetActive(false);
        }

        void CreateMenuItems()
        {
            for (int i = 0; i < items.Count; i++) 
            {
                CreateMenuItem(items[i]);
            }
        }

        public void ToggleShop()
        {
            // here lets check if the shop pannel is active in the hierachy (enabled)
            // if it is we want to turn the panel off, if it's active let's turn it off.
            // we can use the SetActive function of the shop pannel
            shopPannel.SetActive(!shopPannel.activeInHierarchy);
        }

        public void UpdateWallet(int amount)
        {
            // here lets set the walletValue text string to be our $ amount
            walletValue.text = "$" + amount;
        }

        void CreateMenuItem(ShopItem item)
        {
            if (playerInventory == null || shopItemPrefab == null)
            {
                return;
            }

            // so here let's instantiate a shop item prefab, and pass in the shop item parent.
            GameObject clone = Instantiate(shopItemPrefab, shopItemParent);
            // here lets search the clone for the ShopItemUI script and call the setup function
            // lets pass in the item and playerInventory.
            // this is so the item can be bought.
            clone.GetComponent<ShopItemUI>().SetUp(item,playerInventory);
            // finally lets add our clone to our allshopitem instances so we can keep track of em.
            allShopItemInstances.Add(clone);
        }
        
        public bool IsShopOpen()
        {
            // here let's return true or false if the shop pannel is activeInHieracy.
            return shopPannel.activeInHierarchy;
        }
    }
}
