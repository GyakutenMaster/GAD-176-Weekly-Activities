using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD176.WeeklyActivities.WeekNine
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
        protected TankUIManager tankUIManager;
        protected TankSpawnManager tankSpawnManager;
        protected Rigidbody rb;

        // Property to access and modify the player's score
        public int PlayerScore
        {
            get { return playerScore; }
            set
            {
                playerScore = value;
                // Update the score on the UI
                if (!tankUIManager)
                {
                    tankUIManager = FindObjectOfType<TankUIManager>();
                }
                if (tankUIManager != null)
                {
                    tankUIManager.UpdateScore(playerNumber, playerScore);
                }
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
        }

        // Called when the object is disabled. Deinitializes the tank brain.
        private void OnDisable()
        {
            tankBrain.DeInit();
        }

        /// <summary>
        /// Start is called before the first frame update. Sets up various components of the tank.
        /// </summary>
        void Start()
        {
            PlayerScore = 0;

            // Find the TankSpawnManager if not assigned
            if (!tankSpawnManager)
            {
                tankSpawnManager = FindObjectOfType<TankSpawnManager>();
            }

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
                // lets use scriptable object Instantiate to make a copy of the tank brain.
                tankBrain = ScriptableObject.Instantiate(tankBrain);
                // lets then call the Init Function of the tank brain scriptable object.
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
                // lets use scriptable object Instantiate to make a copy of the tank health.
                tankHealth = ScriptableObject.Instantiate(tankHealth);
                // lets then call the Setup Function of the tank health scriptable object.
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
                // lets use scriptable object Instantiate to make a copy of the tank motor.
                tankMotor = ScriptableObject.Instantiate(tankMotor);
                // lets then call the Setup Function of the tank motor scriptable object.
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
                // lets use scriptable object Instantiate to make a copy of the tank main gun.
                tankMainGun = ScriptableObject.Instantiate(tankMainGun);
                // lets then call the Setup Function of the tank main gun scriptable object.
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
                // lets access the tankbrain and call the return key value and pass in the Brain.TankAction.Movement.
                float tankMoveInput = tankBrain.ReturnKeyValue(Brain.TankAction.Movement);
                // lets access the tankbrain and call the return key value and pass in the Brain.TankAction.Rotation.
                float tankRotateInput = tankBrain.ReturnKeyValue(Brain.TankAction.Rotation);
                // lets access the tankbrain and call the return key value and pass in the Brain.TankAction.Fire.
                float tankShootInput = tankBrain.ReturnKeyValue(Brain.TankAction.Fire);

                // here lets access the tankMotor and call the HandleMovement function and pass in the tankmove and the tank rotate
                tankMotor.HandleMovement(tankMoveInput, tankRotateInput);

                // here lets call the tank main gun UpdateMainGun function and apss in the tank shoot input.
                tankMainGun.UpdateMainGun(tankShootInput);
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
        public void TakeDamage(float AmountOfDamage)
        {
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

            if (tankSpawnManager)
            {
                tankSpawnManager.TankDied(gameObject);
            }
        }
    }
}