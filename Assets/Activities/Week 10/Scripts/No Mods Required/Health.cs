using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekTen
{
    /// <summary>
    /// Handles everything in regards to our tanks health system
    /// </summary>
    [CreateAssetMenu(fileName = "Week 10", menuName = "Week 10/Health", order = 0)]
    public class Health : ScriptableObject
    {
        [SerializeField] protected float minHealth = 0; // our min health
        [SerializeField] protected float maxHealth = 100; // our max health
        protected float currentHealth; // our current health
        [SerializeField] protected Color fullHealthColour = Color.green; // our full health colour
        [SerializeField] protected Color zeroHealthColour = Color.red; // colour of no health
        protected Transform tankTransform; // reference to the tank that this script is attached to

        [SerializeField] protected GameObject tankHealthBarPrefab;
        protected GameObject healthbarInstance;
        protected Slider healthSlider; // reference to the health Slider
        protected Image fillImage; // reference to the fill image component of our slider;.

        public float CurrentHealth
        {
            get
            {
                return currentHealth; // return our current health
            }
            set
            {
                currentHealth = value; // set our currenthealth to the value coming in.

                currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth); // making sure that what our damage is, it clamps it between 0 and 100

                if (healthSlider != null)
                {
                    healthSlider.value = CurrentHealth;

                    if (fillImage != null)
                    {
                        // if there is a fill image then let's change its colour to match our current health
                        fillImage.color = Color.Lerp(zeroHealthColour, fullHealthColour, CurrentHealth / maxHealth);
                        // move towards the colour at the rate of our health/maxhealth = a % of our health
                    }
                }
                else
                {
                    Debug.Log("There is no health slider, was this intentional?");
                }
            }
        }


        public void SetUp(Transform TankTransform)
        {
            tankTransform = TankTransform;
            SetupHealthBar();
            SetToMaxHealth();
        }
        public void SetToMaxHealth()
        {
            CurrentHealth = maxHealth; // set our current health to max health
        }


        protected void SetupHealthBar()
        {
            if (tankHealthBarPrefab && !healthbarInstance && tankTransform)
            {
                healthbarInstance = Instantiate(tankHealthBarPrefab, tankTransform);
                healthSlider = healthbarInstance.GetComponentInChildren<Slider>();

                if (healthSlider != null)
                {
                    if (healthSlider.fillRect != null)
                    {
                        fillImage = healthSlider.fillRect.transform.GetComponent<Image>(); // grab a reference to our health slider image
                        Debug.Log("Got instance");
                    }
                    else
                    {
                        Debug.LogError("There is no health slider image");
                    }
                }
            }
        }

        /// <summary>
        /// Applies the amount of health change, if negative it applies damage
        /// If positive it applies health
        /// </summary>
        /// <param name="Amount"></param>
        public void ApplyHealthChange(float Amount)
        {
            //Debug.Log(Amount);
            CurrentHealth += Amount; // increase our health by the amount
        }

        /// <summary>
        /// Returns true or false if the object is dead.
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
    }
}
