using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEleven.Completed
{
    /// <summary>
    /// Handles everything related to tank movement.
    /// </summary>
    [CreateAssetMenu(fileName = "Week 11", menuName = "Completed/Week 11/Motor", order = 0)]
    public class Motor : ScriptableObject
    {
        [SerializeField] protected float speed = 12f; // The speed at which the tank moves.
        [SerializeField] protected float turnSpeed = 180f; // The speed at which the tank can turn in degrees per second.

        [SerializeField] protected ParticleEffects motorMoveEfffects; // Particle effects for tank movement.
        [SerializeField] protected SoundEffect engineIdleSound; // Sound effect for idle engine.
        [SerializeField] protected SoundEffect engineMoveSound; // Sound effect for moving engine.

        protected Rigidbody rigidbody; // Reference to the tank's rigidbody.
        protected bool enableMovement = true; // Flag to determine whether the tank can accept input for movement.
        protected Transform objectReference; // Reference to the tank game object.
        protected AudioSource motorAudioSource;

        public Rigidbody ObjectRigidbody
        {
            get
            {
                if (!rigidbody && objectReference && objectReference.GetComponent<Rigidbody>())
                {
                    rigidbody = objectReference.GetComponent<Rigidbody>(); // Get a reference to the tank's rigidbody.
                }

                if (!rigidbody)
                {
                    Debug.Log("There is no Rigidbody attached. Was this intentional?");
                }

                return rigidbody;
            }
        }

        /// <summary>
        /// Handles the setup of the tank movement script.
        /// </summary>
        /// <param name="Tank">Transform of the tank.</param>
        public void SetUp(Transform Tank)
        {
            objectReference = Tank;
            SetUpVisuals();
            SetUpSounds();
            EnableTankMovement(false);
        }

        void SetUpVisuals()
        {
            if (motorMoveEfffects)
            {
                motorMoveEfffects = ScriptableObject.Instantiate(motorMoveEfffects);
                motorMoveEfffects.SetUpEffects(objectReference, true); // Set up tank effects.
            }
        }

        void SetUpSounds()
        {
            if (objectReference)
            {
                motorAudioSource = objectReference.GetComponent<AudioSource>();
            }
            if (engineIdleSound)
            {
                engineIdleSound = ScriptableObject.Instantiate(engineIdleSound);
            }
            if (engineMoveSound)
            {
                engineMoveSound = ScriptableObject.Instantiate(engineMoveSound);
            }
        }

        /// <summary>
        /// Plays engine sounds based on movement and rotation inputs.
        /// </summary>
        /// <param name="MoveInput">Forward movement input.</param>
        /// <param name="RotationInput">Rotation input.</param>
        public void PlayEngineSounds(float MoveInput, float RotationInput)
        {
            if (Mathf.Abs(MoveInput) < 0.1f && Mathf.Abs(RotationInput) < 0.1f)
            {
                // Play idle sound.
                if (engineIdleSound)
                {
                    engineIdleSound.PlaySound(motorAudioSource);
                }
            }
            else
            {
                if (engineMoveSound)
                {
                    // Play move sound.
                    engineMoveSound.PlaySound(motorAudioSource);
                }
            }
        }

        /// <summary>
        /// Enables or disables tank movement.
        /// </summary>
        /// <param name="Enabled">Flag to determine whether tank movement is enabled.</param>
        public void EnableTankMovement(bool Enabled)
        {
            enableMovement = Enabled;
        }

        /// <summary>
        /// Handles the movement of the tank.
        /// </summary>
        public void HandleMovement(float ForwardMovement, float RotationMovement)
        {
            // If movement is disabled, do nothing.
            if (enableMovement == false)
            {
                return;
            }

            Move(ForwardMovement);
            Turn(RotationMovement);

            PlayEngineSounds(ForwardMovement, RotationMovement);
        }

        /// <summary>
        /// Moves the tank forward and backward.
        /// </summary>
        private void Move(float ForwardMovement)
        {
            if (!ObjectRigidbody)
            {
                return;
            }
            // Create a vector based on the forward vector of the tank, move it forward or backward based on the key input,
            // multiplied by the speed, multiplied by the time between frames rendered to make it smooth.
            Vector3 movementVector = objectReference.forward * ForwardMovement * speed * Time.deltaTime;
            ObjectRigidbody.MovePosition(ObjectRigidbody.position + movementVector); // Move the rigidbody based on the current position + movement vector.
        }

        /// <summary>
        /// Rotates the tank on the Y axis.
        /// </summary>
        private void Turn(float RotationalAmount)
        {
            if (!ObjectRigidbody)
            {
                return;
            }

            // Get the key input value, multiply it by the turn speed, multiply it by the time between frames.
            float turnAngle = RotationalAmount * turnSpeed * Time.deltaTime; // The angle in degrees we want to turn our tank.
            Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0); // Convert the angle into a quaternion for rotation.

            // Update the rigidbody with this
            ObjectRigidbody.MoveRotation(ObjectRigidbody.rotation * turnRotation); // rotate our rigidbody based on our input.
        }
    }
}