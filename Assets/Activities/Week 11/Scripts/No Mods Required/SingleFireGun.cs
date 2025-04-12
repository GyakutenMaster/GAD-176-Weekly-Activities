using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekEleven
{
    /// <summary>
    /// Handles firing the main weapon of the tank
    /// </summary>
    [CreateAssetMenu(fileName = "Week 11", menuName = "Week 11/Gun", order = 0)]
    public class SingleFireGun : ScriptableObject
    {
        [SerializeField] protected string firePointName = "FirePoint";
        protected Transform firePointTransform; // reference to the main gun of the tank
        [SerializeField] protected GameObject tankShellPrefab; // reference to the tank prefab we want to fire

        [SerializeField] protected float minLaunchForce = 15f; // the minimum amount of force for our weapon
        [SerializeField] protected float maxLaunchForce = 30f; // the maximum amount of force for our weapon
        [SerializeField] protected float maxChargeTime = 0.75f; // the maximum amount of time we will allow to charge up and fire

        [SerializeField] protected GameObject mainGunArrowIndicatorPrefab; // a reference to the main gun slider
        protected GameObject mainGunArrowInstance; // a reference to the main gun slider
        protected Slider mainGunArrowIndicator; // a reference to the main gun slider

        protected float currentLaunchForce; // the force we should use to fire our shell
        protected float chargeSpeed; // how fast we should charge up our weapon
        protected bool weaponFired; // have we just fired our weapon?

        protected AudioSource weaponSystemSource; // reference to the audio source for the main gun
        [SerializeField] protected SoundEffect chargingEffect; // creating a new instance of our tank sound effects class
        [SerializeField] protected SoundEffect shotFiredEffect; // creating a new instance of our tank sound effects class
        protected bool enableShooting; // should we be allowed to fire?
        protected Transform objectTransform;

        /// <summary>
        /// Sets up all the necessary variables for our main gun script
        /// </summary>
        public void SetUp(Transform Tank)
        {
            objectTransform = Tank;
            SetUpUI();
            SetUpAudioEffects();

            currentLaunchForce = minLaunchForce; // set our current launch force to the min
            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime; // get the range between the max and min, and divide it by how quickly we charge
            EnableShooting(false); // disable shooting
        }

        // Set up UI elements associated with the main gun
        void SetUpUI()
        {
            if (objectTransform != null)
            {
                firePointTransform = objectTransform.Find(firePointName);

                // Instantiate main gun arrow indicator prefab
                if (mainGunArrowIndicatorPrefab && !mainGunArrowInstance)
                {
                    mainGunArrowInstance = Instantiate(mainGunArrowIndicatorPrefab, objectTransform);
                }

                // Set up main gun arrow slider
                if (mainGunArrowIndicator == null && mainGunArrowInstance)
                {
                    mainGunArrowIndicator = mainGunArrowInstance.GetComponentInChildren<Slider>();
                    if (mainGunArrowIndicator != null)
                    {
                        mainGunArrowIndicator.minValue = minLaunchForce; // set the min and max programmatically
                        mainGunArrowIndicator.maxValue = maxLaunchForce;
                    }
                }
            }
        }

        // Set up audio effects associated with the main gun
        void SetUpAudioEffects()
        {
            if (!weaponSystemSource)
            {
                if (firePointTransform.GetComponent<AudioSource>() == null)
                {
                    weaponSystemSource = firePointTransform.AddComponent<AudioSource>();
                    weaponSystemSource.playOnAwake = false;
                    weaponSystemSource.loop = false;
                }
            }

            // Instantiate sound effect instances
            if (chargingEffect)
            {
                chargingEffect = ScriptableObject.Instantiate(chargingEffect);
            }

            if (shotFiredEffect)
            {
                shotFiredEffect = ScriptableObject.Instantiate(shotFiredEffect);
            }
        }

        /// <summary>
        /// Called to enable/disable shooting
        /// </summary>
        /// <param name="Enabled"></param>
        public void EnableShooting(bool Enabled)
        {
            enableShooting = Enabled;
        }

        /// <summary>
        /// Updates the state of the main gun based on the input value.
        /// </summary>
        /// <param name="MainGunValue">Input value for the main gun.</param>
        public void UpdateMainGun(float MainGunValue)
        {
            if (enableShooting != true)
            {
                return; // don't do anything
            }

            if (currentLaunchForce >= maxLaunchForce && !weaponFired)
            {
                // If we are at max charge and haven't fired the weapon
                currentLaunchForce = maxLaunchForce;
                FireWeapon(); // fire our gun
            }
            // If the main gun button is pressed and we haven't fired
            else if (MainGunValue > 0 && !weaponFired)
            {
                // We want to charge up our weapon
                currentLaunchForce += chargeSpeed * Time.deltaTime; // increase the current force

                // Play a charging up sound effect
                if (!weaponSystemSource.isPlaying)
                {
                    chargingEffect.PlaySound(weaponSystemSource);
                    Debug.Log("Charging");
                }
            }
            // If the main gun button is released and we haven't fired and current launch force is above min
            else if (MainGunValue <= 0 && !weaponFired && currentLaunchForce > minLaunchForce + 1)
            {
                // We've released our button
                // We want to fire our weapon
                FireWeapon(true);
            }
            // If the main gun button is released and weapon has been fired
            else if (MainGunValue <= 0 && weaponFired)
            {
                weaponFired = false;
            }

            // Update the main gun arrow indicator
            if (mainGunArrowIndicator)
            {
                mainGunArrowIndicator.value = currentLaunchForce; // set our arrow back to min at all times
            }
        }

        /// <summary>
        /// Fires the main weapon.
        /// </summary>
        /// <param name="ButtonReleased">Indicates whether the fire button has been released.</param>
        private void FireWeapon(bool ButtonReleased = false)
        {
            weaponFired = true; // we have fired our weapon

            // Spawn a tank shell at the main gun transform and match the rotation of the main gun
            GameObject clone = Object.Instantiate(tankShellPrefab, firePointTransform.position, firePointTransform.rotation);

            // If the clone has a rigidbody, add some velocity to it to make it fire
            if (clone.GetComponent<Rigidbody>())
            {
                clone.GetComponent<Rigidbody>().velocity = currentLaunchForce * firePointTransform.forward; // make the velocity of our bullet go in the direction of our gun at the launch force
            }

            Object.Destroy(clone, 5f);

            // Play the firing sound effect
            shotFiredEffect.PlaySound();
            chargingEffect.StopSound(); // stop charging up
            currentLaunchForce = minLaunchForce;

            // only reset our weapon if we have released our fire button, don't allow it if we overcharged
            if (ButtonReleased)
            {
                weaponFired = false;
            }
        }
    }
}
