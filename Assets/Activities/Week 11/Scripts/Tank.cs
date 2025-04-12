using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD176.WeeklyActivities.WeekEleven
{
    // Enumeration to represent player numbers
    public enum PlayerNumber { One = 1, Two = 2, Three = 3, Four = 4 }

    /// <summary>
    /// The main class of our tank.
    /// Everything should be run from here.
    /// </summary>
    public class Tank : MonoBehaviour
    {
        // Serialized fields can be edited in the Unity Editor
        [SerializeField] protected bool enableTankMovement = false;
        [SerializeField] protected PlayerNumber playerNumber;
        [SerializeField] protected Brain tankBrain;
        [SerializeField] protected Health tankHealth;
        [SerializeField] protected Motor tankMotor;
        [SerializeField] protected SingleFireGun tankMainGun;
        [SerializeField] protected GameObject explosionPrefab;

        protected int playerScore;
        protected Rigidbody rb;

        // Property to access and modify the player's score
        public int PlayerScore
        {
            get { return playerScore; }
            set
            {
                playerScore = value;
                // lets access the tank ui mangaer Instance and call the update score function.
                TankUIManager.Instance.UpdateScore(CurrentPlayerNumber, playerScore);
            }
        }

        // Property to access and modify the current player number
        public PlayerNumber CurrentPlayerNumber
        {
            get { return playerNumber; }
            set { playerNumber = value; }
        }

        // Called when the object is enabled. Initializes the tank brain.
        private void OnEnable()
        {
            if (tankBrain)
            {
                tankBrain.Init(transform);
            }

            // subscribe our take damage function to the take damage event.
            GameEvents.OnTakeDamageEvent += TakeDamage;
            // subcribe our enable input to our enable playermovement event
            GameEvents.EnablePlayerMovementEvent += EnableInput;
        }

        // Called when the object is disabled. Deinitializes the tank brain.
        private void OnDisable()
        {
            tankBrain.DeInit();
            // unsubscribe our take damage function to the take damage event.
            GameEvents.OnTakeDamageEvent -= TakeDamage;
            // unsubcribe our enable input to our enable playermovement event
            GameEvents.EnablePlayerMovementEvent -= EnableInput;

        }

        /// <summary>
        /// Start is called before the first frame update. Sets up various components of the tank.
        /// </summary>
        void Start()
        {
            PlayerScore = 0;
            // Set up various components of the tank
            SetUpBrain();
            SetUpHealth();
            SetUpTankMotor();
            SetUpTankGun();
            rb = GetComponent<Rigidbody>();
            HandleMouseCursor();
        }

        /// <summary>
        /// Instantiates and initializes the tank brain.
        /// </summary>
        void SetUpBrain()
        {
            if (tankBrain)
            {
                tankBrain = ScriptableObject.Instantiate(tankBrain);
                tankBrain.Init(transform);
            }
        }

        /// <summary>
        /// Instantiates and initializes the tank health.
        /// </summary>
        void SetUpHealth()
        {
            if (tankHealth)
            {
                tankHealth = ScriptableObject.Instantiate(tankHealth);
                tankHealth.SetUp(transform);
            }
        }

        /// <summary>
        /// Instantiates and initializes the tank motor.
        /// </summary>
        void SetUpTankMotor()
        {
            if (tankMotor)
            {
                tankMotor = ScriptableObject.Instantiate(tankMotor);
                tankMotor.SetUp(transform);
            }
        }

        /// <summary>
        /// Instantiates and initializes the tank gun.
        /// </summary>
        void SetUpTankGun()
        {
            if (tankMainGun)
            {
                tankMainGun = ScriptableObject.Instantiate(tankMainGun);
                tankMainGun.SetUp(transform);
            }
        }

        /// <summary>
        /// Handles mouse cursor visibility and lock state.
        /// </summary>
        void HandleMouseCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            // Check if tankBrain, tankMotor, and tankMainGun are assigned
            if (tankBrain && tankMotor && tankMainGun)
            {
                // Pass input values to tankMotor to handle movement
                tankMotor.HandleMovement(tankBrain.ReturnKeyValue(Brain.TankAction.Movement), tankBrain.ReturnKeyValue(Brain.TankAction.Rotation));

                // Update main gun based on input
                tankMainGun.UpdateMainGun(tankBrain.ReturnKeyValue(Brain.TankAction.Fire));
            }
        }

        /// <summary>
        /// Enables or disables input for the tank.
        /// </summary>
        public void EnableInput(bool enabled)
        {
            if (tankMotor)
            {
                tankMotor.EnableTankMovement(enabled);
            }
            if (tankMainGun)
            {
                tankMainGun.EnableShooting(enabled);
            }
        }

        /// <summary>
        /// Called when the tank takes damage. Applies damage to the tank health.
        /// </summary>
        /// <param name="AmountOfDamage"></param>
        public void TakeDamage(Transform objectHit, float AmountOfDamage)
        {

            // if we aren't the object that was hit, just return.
            if(objectHit != transform)
            {
                return;
            }
            //otherwise do damage.
            tankHealth.ApplyHealthChange(AmountOfDamage);

            if (tankHealth.IsDead())
            {
                Dead();
            }
        }

        /// <summary>
        /// Respawns the tank by resetting its state.
        /// </summary>
        public void Respawn()
        {
            // Set the game object active, disable input, reset health, and cancel velocity
            gameObject.SetActive(true);
            EnableInput(false);
            tankHealth.SetToMaxHealth();

            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        /// <summary>
        /// Called when the tank is destroyed. Instantiates an explosion effect and notifies the spawn manager.
        /// </summary>
        private void Dead()
        {
            GameObject clone = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            Destroy(clone, 2);
            gameObject.SetActive(false);

            // lets access the tank spawn manager instance and call the tank died function
            TankSpawnManager.Instance.TankDied(gameObject);
        }
    }
}