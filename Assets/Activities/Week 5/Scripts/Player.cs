using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFive
{
    public class Player : MonoBehaviour
    {
        public float maxSpeed = 5f;
        public float accelerationSpeed = 2f; // Adjust the acceleration speed
        public float decelerationForce = 5f; // Adjust the deceleration force
        public float boostForce = 20f;

        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            MoveSpaceship();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Boost();
            }
        }

        void MoveSpaceship()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // here let's normalise the movement vector so we can move at a consistent speed.
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;

            // let's lerp the rigidbodies current velocity, by out movement times our max speed, smoothly by using delta time multiplied by the acceleration speed.
            rb.velocity = Vector3.Lerp(rb.velocity, movement * maxSpeed, Time.deltaTime * accelerationSpeed);

            // lets's add a constanst deceleration force by adding on the rigidbodies normalised velocity multiplied by a NEGATIVE deceleration force.
            rb.AddForce(-rb.velocity.normalized * decelerationForce);
        }

        void Boost()
        {
            // Check if Rigidbody is not null
            if (rb != null)
            {
                // here lets add force in the direction of world space up multiplied by a boost force.
                // and lets use a force mode impulse for a sudden addition of force.
                rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody component not found on the player object.");
            }
        }
    }
}