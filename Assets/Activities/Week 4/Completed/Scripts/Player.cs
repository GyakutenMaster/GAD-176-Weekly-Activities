using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour.Completed
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform endPoint;
        [SerializeField] private float playerHealth;
        [SerializeField] private float playerMaxHealth = 100;
        [SerializeField] private float horizontalSpeed = 5f;
        [SerializeField] private float verticalSpeed = 5f;
        private float totalDistance;
        [SerializeField] private float sliderModelOffset = 2;
        private UserInterface userInterface;

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
                if (currentEquipedWeapon)
                {
                    // here lets access the current weapon and call the equip function, but let's unequip the current weapon.
                    currentEquipedWeapon.Equip(false);
                }
                // here we are setting the current weapon to a new one
                currentEquipedWeapon = value;
                // then let's equip the newly equiped weapon using the equip function
                currentEquipedWeapon.Equip(true);
            }
        }


        private void Awake()
        {
            // hack to unpause the game.
            Time.timeScale = 1;

            // find a reference to the user interface in the scene, find object will help
            userInterface = FindObjectOfType<UserInterface>();

            // set the player health to be the max health using ChangeHealth
            ChangeHealth(playerMaxHealth);

            if (endPoint)
            {
                // here lets calculate the total distance, which will be the y position of the player minus the y position of the end points position.
                // plus the slider offset to account for the size of the UI element, make sure you use parenthesis around the minus.
                totalDistance = (transform.position.y - endPoint.position.y) + sliderModelOffset;
            }

            // here lets call the UpdateDistance Progress function.
            UpdateDistanceProgress();
            // lets call the unequip all weapons function to hide them all.
            UnequipAllWeapons();
            if (allWeapons.Count > 0)
            {
                CurrentWeapon = allWeapons[0];
            }
        }

        void Update()
        {
            Move();
            UpdateDistanceProgress();
            HandleWeaponInput();
        }

        void HandleWeaponInput()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // here let's call the fire function of the current weapon equiped.
                currentEquipedWeapon.Fire();
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                CurrentWeapon = allWeapons[GetNextWeapon(1)];
            }
        }

        public float CurrentHealth()
        {
            return playerHealth;
        }

        void UnequipAllWeapons()
        {
            for (int i = 0; i < allWeapons.Count; i++)
            {
                // here lets access the current element we are up to and call equip, and lets hide it.
                allWeapons[i].Equip(false);
            }
        }

        public void ChangeHealth(float amount)
        {
            playerHealth += amount;

            // here lets use a mathf clamp to clamp the player healths current value between 0 and the max value.
            playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

            if (userInterface)
            {
                userInterface.UpdateHealth(playerHealth, playerMaxHealth);
            }

            if (playerHealth <= 0)
            {
                Debug.Log("Game Over");
                Time.timeScale = 0;
            }
        }
        private void UpdateDistanceProgress()
        {
            if (endPoint != null && userInterface != null)
            {
                // here lets calculate the current distance, which will be the y position of the player minus the y position of the end points position.
                // plus the slider offset to account for the size of the UI element, make sure you use parenthesis around the minus.
                float currentDistance = transform.position.y - endPoint.position.y + sliderModelOffset;
                userInterface.UpdateProgressSlider(currentDistance, totalDistance);
            }
        }

        private void Move()
        {
            // Get user input for movement (assuming arrow keys)
            float horizontalInput = Input.GetAxis("Horizontal");

            // Create a movement vector based on user input
            Vector3 movement = transform.up + new Vector3(horizontalInput, 0f, 0f);

            // Normalize the vector to ensure consistent speed in all directions
            movement.Normalize();

            // Move the spaceship based on the movement vector
            transform.Translate(new Vector3(movement.x * horizontalSpeed, movement.y * verticalSpeed, movement.z) * Time.deltaTime);
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