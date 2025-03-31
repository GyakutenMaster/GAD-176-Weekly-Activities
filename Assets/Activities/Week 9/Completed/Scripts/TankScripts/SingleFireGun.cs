using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekNine.Completed
{
    /// <summary>
    /// Handles firing the main weapon of the tank
    /// </summary>
    [CreateAssetMenu(fileName = "Week 9", menuName = "Completed/Week 9/Gun", order = 0)]
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
            // here lets call the set up ui function.
            SetUpUI();
            // lets call the setup audio effects function
            SetUpAudioEffects();

            // lets set the currentLaunch force to be the min force.
            currentLaunchForce = minLaunchForce; // set our current launch force to the min
               
            // set the charge speed set to the maxLaunchForce minus the minLaunchForce divided by the max charge time, it's important to use parenthesis around the minus.
            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime; // get the range between the max and min, and divide it by how quickly we charge

            // lets disable shooting by calling the enable shooting function.
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
                    // Spawn in a maingun arrow prefab, and lets parent it to the objectTransform.
                    mainGunArrowInstance = Instantiate(mainGunArrowIndicatorPrefab, objectTransform);
                }

                // Set up main gun arrow slider
                if (mainGunArrowIndicator == null && mainGunArrowInstance)
                {
                    mainGunArrowIndicator = mainGunArrowInstance.GetComponentInChildren<Slider>();
                    if (mainGunArrowIndicator != null)
                    {
                        // lets set the main gun arrow indicators minValue to be the min launch force.
                        mainGunArrowIndicator.minValue = minLaunchForce; // set the min and max programmatically
                        // lets set the main gun arrow indicators maxValue to be the max launch force.
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
                // lets use ScriptableObject.Instante and make a copy of the chargingEffect.
                chargingEffect = ScriptableObject.Instantiate(chargingEffect);
            }

            if (shotFiredEffect)
            {
                // lets use ScriptableObject.Instante and make a copy of the shotFiredEffect.
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
                // lets set the current launchForce to be the maxLaunchForce
                // we do this to make sure the slider goes to the max.
                currentLaunchForce = maxLaunchForce;
                // now let's fire the gun Fire Weapon function
                FireWeapon(); // fire our gun
            }
            // If the main gun button is pressed and we haven't fired
            else if (MainGunValue > 0 && !weaponFired)
            {
                // lets increase the current launch force by the chargeSpeed multiplied by the Time.deltatime.
                currentLaunchForce += chargeSpeed * Time.deltaTime; // increase the current force

                // Play a charging up sound effect
                if (!weaponSystemSource.isPlaying)
                {
                    // here lets call the playsound function of the chargingeffect scriptable object.
                    // your going to want to pass in the weaponSystemSource to play it from.
                    chargingEffect.PlaySound(weaponSystemSource);
                    Debug.Log("Charging");
                }
            }
            // If the main gun button is released and we haven't fired and current launch force is above min
            else if (MainGunValue <= 0 && !weaponFired && currentLaunchForce > minLaunchForce + 1)
            {
                // here let's call the fireweapon function and pass in that the button was released.
                FireWeapon(true);
            }
            // If the main gun button is released and weapon has been fired
            else if (MainGunValue <= 0 && weaponFired)
            {
                // here lets set the weapon fired bool to false to say we've fired.
                weaponFired = false;
            }

            // Update the main gun arrow indicator
            if (mainGunArrowIndicator)
            {
                // here lets set the value of the main gun arrow indicator to the current launch force.
                mainGunArrowIndicator.value = currentLaunchForce; // set our arrow back to min at all times
            }
        }

        /// <summary>
        /// Fires the main weapon.
        /// </summary>
        /// <param name="ButtonReleased">Indicates whether the fire button has been released.</param>
        private void FireWeapon(bool ButtonReleased = false)
        {
            // here lets set the weapon fired to true.
            weaponFired = true; // we have fired our weapon

            // here lets use Object.Instantiate to spawn in the tankshell prefab at the firepoint transofrm and using the firepoint transform's rotation.
            GameObject clone = Object.Instantiate(tankShellPrefab, firePointTransform.position, firePointTransform.rotation);

            // If the clone has a rigidbody, add some velocity to it to make it fire
            if (clone.GetComponent<Rigidbody>())
            {
                clone.GetComponent<Rigidbody>().velocity = currentLaunchForce * firePointTransform.forward; // make the velocity of our bullet go in the direction of our gun at the launch force
            }

            Object.Destroy(clone, 5f);

            // Lets call the playsound function of the shotFiredEffect
            shotFiredEffect.PlaySound();
            // lets call the stop sound function of the charging effect.
            chargingEffect.StopSound(); // stop charging up
            // lets set the current launch force to be the min launch force.
            currentLaunchForce = minLaunchForce;

            // only reset our weapon if we have released our fire button, don't allow it if we overcharged
            if (ButtonReleased)
            {
                // lets set the weaponFired to be false
                weaponFired = false;
            }
        }
    }
}
