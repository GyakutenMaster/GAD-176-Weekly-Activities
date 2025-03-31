using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekEight.Completed
{
    [CreateAssetMenu(fileName = "Shop Item", menuName = "Completed/Week 8/Shop/Shop Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected int cost;
        [SerializeField] protected Sprite itemSprite;
        [SerializeField] protected ScriptableObject weaponScriptableObject;

        public string ItemName
        {
            // return the item name
            get { return itemName;}
        }

        public int Cost
        {
            // return the cost
            get { return cost; }
        }

        public Sprite ItemSprite
        {
            // return the itemsprite.
            get { return itemSprite;}
        }

        public ScriptableObject WeaponScriptableObject
        {
            // return the scriptable object
            get { return weaponScriptableObject;}
        }
    }
}
