using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekNine.Completed
{
    /// <summary>
    /// Handles everything related to tank movement.
    /// </summary>
    [CreateAssetMenu(fileName = "Week 9", menuName = "Completed/Week 9/Motor", order = 0)]
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
            // lets call the set up visuals function
            SetUpVisuals();
            // lets call the set up sounds function
            SetUpSounds();
            // lets disable the movement by calling the enable tank movement.
            EnableTankMovement(false);
        }

        void SetUpVisuals()
        {
            if (motorMoveEfffects)
            {
                // lets call the ScriptableObject.Instantiate and make a copy of the motorMoveEffects
                motorMoveEfffects = ScriptableObject.Instantiate(motorMoveEfffects);
                // lets call the set up effects function of the motorMoveEffects, you'll need to pass in the object reference which is the tank
                // lets also make sure we play on spawn.
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
                // lets call the ScriptableObject.Instantiate and make a copy of the engineIdleSound
                engineIdleSound = ScriptableObject.Instantiate(engineIdleSound);
            }
            if (engineMoveSound)
            {
                // lets call the ScriptableObject.Instantiate and make a copy of the engineMoveSound
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
                    // lets access the engine idlesound and call the playsound function and pass in the motoraudiosource.
                    engineIdleSound.PlaySound(motorAudioSource);
                }
            }
            else
            {
                if (engineMoveSound)
                {
                    // lets access the engine move sound and call the playsound function and pass in the motoraudiosource.
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

            // lets call the move function and pass in the forward movement.
            Move(ForwardMovement);
            // lets call the turn function and pass in the rotation movement.
            Turn(RotationMovement);
            // lets call the play engine sounds and pass in the forward and rotational movement.
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
            // lets set our movement vector be the objectreferences forward direction, multiplied by our forward movement mulitplied by the speed
            // multiplied by the time.deltatime.
            Vector3 movementVector = objectReference.forward * ForwardMovement * speed * Time.deltaTime;
            // lets call the moveposition function of our ObjectRigibody, and pass in the rigibodies position plus the movement vector.
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

            // lets take the rotationalAmount multiplied by the turn speed multiplied by the Time.deltatime.
            float turnAngle = RotationalAmount * turnSpeed * Time.deltaTime; // The angle in degrees we want to turn our tank.
            // lets set the turn rotation to be the quaternion.euler and pass in th turnAngle as the y axis, the others can be 0.
            Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0); // Convert the angle into a quaternion for rotation.


            // lets call the moverotation function of the objectrigidbody and pass in the current rotation of the rigidbody multiplied by the turn rotation.
            ObjectRigidbody.MoveRotation(ObjectRigidbody.rotation * turnRotation); // rotate our rigidbody based on our input.
        }
    }
}