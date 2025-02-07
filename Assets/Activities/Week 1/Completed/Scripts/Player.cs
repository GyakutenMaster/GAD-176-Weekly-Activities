using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekOne.Completed
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
                // start by setting the total distance to be the player Y pos, minus the End pos y, plus the model offset.
                // your gonna want to use our parenthesis here.
                totalDistance = (transform.position.y - endPoint.position.y) + sliderModelOffset;
            }

            UpdateDistanceProgress();
        }

        void Update()
        {
            Move();
            UpdateDistanceProgress();
        }

        public float CurrentHealth()
        {
            return playerHealth;
        }

        public void ChangeHealth(float amount)
        {
            // have the amount coming in be added to the playerhealth
            playerHealth += amount;

            // here let's clamp the value using Mathf.Clamp and pass in the current health, min and max health
            playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

            if (userInterface)
            {
                userInterface.UpdateHealth(playerHealth, playerMaxHealth);
            }

            // check to see if the player is dead, if so call game over and pause the game using timescale
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
                // here let's calculate the current distance, by taking the player's y position minus the endpoint y position and then add the slider model offset
                // the model offset is to accomoade the sprite.
                float currentDistance = transform.position.y - endPoint.position.y + sliderModelOffset;
                userInterface.UpdateProgressSlider(currentDistance, totalDistance);
            }
        }

        private void Move()
        {
            // Get user input for movement (assuming arrow keys)
            // let's us Input.GetAxis() and grab the "Horizontal" input
            float horizontalInput = Input.GetAxis("Horizontal");

            // Create a movement vector based on user input
            // so lets move upwards but let's also add onto our updwards our horizontal movement for our input for our x axis.
            Vector3 movement = transform.up + new Vector3(horizontalInput, 0f, 0f);

            // Normalize the vector to ensure consistent speed in all directions
            movement.Normalize();

            // Move the spaceship based on the movement vector
            transform.Translate(new Vector3(movement.x * horizontalSpeed, movement.y * verticalSpeed, movement.z) * Time.deltaTime);
        }
    }
}
