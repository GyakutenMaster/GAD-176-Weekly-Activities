using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD176.WeeklyActivities.WeekNine
{
    public class Brain : ScriptableObject
    {
        public enum TankAction { Movement, Rotation, Fire }
        protected float move;
        protected float rotate;
        protected float fire;

        /// <summary>
        /// Initialize the brain with the tank's transform.
        /// </summary>
        /// <param name="tankTransform">Transform of the tank.</param>
        public virtual void Init(Transform tankTransform)
        {
            
        }

        /// <summary>
        /// Deinitialize the brain.
        /// </summary>
        public virtual void DeInit()
        {
          
        }

        /// <summary>
        /// If the value returned is positive, then the positive axis has been pressed for that key.
        /// If the value returned is negative, then the negative axis has been pressed.
        /// If the value is 0, then no button has been pressed.
        /// </summary>
        /// <param name="codeToCheck">The TankAction to check (Movement, Rotation, Fire).</param>
        /// <returns>The current input value of the specified TankAction.</returns>
        public virtual float ReturnKeyValue(TankAction codeToCheck)
        {
            float currentValue = 0; // the current input value of the code to check

            // Switch based on the TankAction to retrieve the corresponding input value
            switch (codeToCheck)
            {
                case TankAction.Movement:
                    {
                        currentValue = move;
                        break;
                    }
                case TankAction.Rotation:
                    {
                        currentValue = rotate;
                        break;
                    }
                case TankAction.Fire:
                    {
                        currentValue = fire;
                        break;
                    }
            }
            return currentValue;
        }
    }
}
