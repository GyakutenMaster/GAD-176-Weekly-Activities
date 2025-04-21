using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD176.WeeklyActivities.WeekTwelve
{
    /// <summary>
    /// This class holds the data for our player controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Week 12", menuName = "Week 12/Player Tank Brain", order = 0)]
    public class PlayerBrain : Brain
    {
        [SerializeField] protected InputActionAsset inputMap;
        protected InputActionAsset inputInstance;
        protected PlayerInput playerInput;

        /// <summary>
        /// Returns the current instance of the input action map.
        /// </summary>
        public InputActionAsset ActionMap
        {
            get
            {
                if (inputInstance == null && inputMap != null)
                {
                    // Create an instance of the input map if it doesn't exist.
                    inputInstance = Instantiate(inputMap);
                }
                return inputInstance;
            }
        }

        /// <summary>
        /// Initializes the player's input system.
        /// </summary>
        /// <param name="tankTransform">Transform of the tank associated with this brain.</param>
        public override void Init(Transform tankTransform)
        {
            if (!playerInput)
            {
                playerInput = tankTransform.GetComponent<PlayerInput>();
            }

            if (playerInput)
            {
                // Set the player's actions to the specified action map.
                playerInput.actions = ActionMap;

                // Set notification behavior to invoke C# events.
                playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;

                // Subscribe to the onActionTriggered event.
                playerInput.onActionTriggered += PlayerInput_onActionTriggered;

                // Activate input for the player.
                playerInput.ActivateInput();

                // Hack to resolve an issue when turning objects on/off.
                playerInput.enabled = false;
                playerInput.enabled = true;
            }
        }

        /// <summary>
        /// Deinitializes the player's input system.
        /// </summary>
        public override void DeInit()
        {
            // Unsubscribe from the onActionTriggered event.
            playerInput.onActionTriggered -= PlayerInput_onActionTriggered;
        }

        /// <summary>
        /// Callback method for handling player input actions.
        /// </summary>
        /// <param name="obj">Callback context for the triggered action.</param>
        private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
        {
            Debug.Log("Action happening");

            if (obj.action.name == "Move")
            {
                // Update the move input based on the y value of the Vector2.
                move = obj.ReadValue<Vector2>().y;
            }
            if (obj.action.name == "Look")
            {
                // Update the rotate input based on the x value of the Vector2.
                rotate = obj.ReadValue<Vector2>().x;
            }
            if (obj.action.name == "Fire")
            {
                // Update the fire input based on the float value.
                fire = obj.ReadValue<float>();
            }
        }
    }
}
