using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwo
{
    [System.Serializable]
    public class Thrusters
    {
        public enum ThrusterTypes { Left, Right, Main };
        // references to my 3 thrusters in my scene.
        [SerializeField] private Transform mainThruster;
        [SerializeField] private Transform leftThruster;
        [SerializeField] private Transform rightThruster;

        // My thrust values for going up and to the side.
        [SerializeField] private float horizontalThrust = 0.9f;
        [SerializeField] private float verticalThrust = 2.2f;

        [SerializeField] private float currentFuel = 15;
        [SerializeField] private float maxFuel = 20;

        [SerializeField] private bool isApplyingThrust = false;
        private LunarLanderUIManager uiManager;
        [SerializeField] private float fuelCost = 0.01f;

        public void SetUp(LunarLanderUIManager current)
        {
            uiManager = current;
            CurrentFuel = currentFuel;
        }

        public float CurrentFuel
        {
            get
            {
                return currentFuel;
            }
            set
            {
                // here let's clamp the value coming in, between 0 and the max fuel.
                currentFuel = value;
                // let's update our fuel UI with the normalised value of our current fuel and our max fuel.
                uiManager.InGameUI.UpdateFuel(0); // normalise our fuel amount
            }
        }
        public bool Thrusting
        {
            get
            {
                return isApplyingThrust;
            }
            set
            {
                isApplyingThrust = value;
            }
        }


        public void AddThrust(Rigidbody2D rigidbodyToApplyTo, ThrusterTypes selectedThruster)
        {
            // If the fuel is less than or equal 0 just return out of this function
            if (CurrentFuel <= 0)
            {
                return;
            }

            Vector2 thrustDirection = Vector2.zero; // temporary direction of thrust
            float currentThrust = 0;
            Transform currentThruster = null;
            isApplyingThrust = true;

            if (selectedThruster == ThrusterTypes.Main)
            {
                // get the direction of thrust that we want to apply, to get a direction of vector we take the starting position, and minus it from the end position;
                // so to get the direction, let's start with the rigibodyToApplyTo's position and minus away the mainThrusters.position.
                // NOTE you have 2D rigibody's position meaning vector 2, and a thurster position vector 3.
                // this means you'll have to convert one to a vector 2, or one to a vector 3.
                // we can cast vectors just like we did with ints and floats.
                thrustDirection = Vector2.zero;

                // We then want to set our thrust to vertical as it is the main thruster and not a side.
                currentThrust = verticalThrust;
                currentThruster = mainThruster;
            }
            else if (selectedThruster == ThrusterTypes.Left)
            {
                // get the direction of thrust that we want to apply, to get a direction of vector we take the starting position, and minus it from the end position;
                // so to get the direction, let's start with the rigibodyToApplyTo's position and minus away the leftThrusters.position.
                // NOTE you have 2D rigibody's position meaning vector 2, and a thurster position vector 3.
                // this means you'll have to convert one to a vector 2, or one to a vector 3.
                // we can cast vectors just like we did with ints and floats.
                thrustDirection = Vector2.zero;

                currentThrust = horizontalThrust;
                currentThruster = leftThruster;
            }
            else if (selectedThruster == ThrusterTypes.Right)
            {
                // get the direction of thrust that we want to apply, to get a direction of vector we take the starting position, and minus it from the end position;
                // so to get the direction, let's start with the rigibodyToApplyTo's position and minus away the rightThrusters.position.
                // NOTE you have 2D rigibody's position meaning vector 2, and a thurster position vector 3.
                // this means you'll have to convert one to a vector 2, or one to a vector 3.
                // we can cast vectors just like we did with ints and floats.
                thrustDirection = Vector2.zero;

                currentThrust = horizontalThrust;
                currentThruster = rightThruster;
            }

            CurrentFuel -= fuelCost;
            LunarLanderGameManager.playerApplyThrust?.Invoke();
            currentThruster.GetComponent<Animator>().SetBool("AllowThrust", true);

            
            // so here, we want to access the rigidbody and use the AddForceAtPosition function.
            // this will required the thurstDirection to be normalised, multiplied by the current thrust
            // and pass in the current thrusters position.
            rigidbodyToApplyTo.AddForceAtPosition(Vector2.zero,Vector2.zero);
        }

        public void CancelAnimation(ThrusterTypes thruster)
        {
            if (thruster == ThrusterTypes.Main)
            {
                mainThruster.GetComponent<Animator>().SetBool("AllowThrust", false);
            }
            else if (thruster == ThrusterTypes.Left)
            {
                leftThruster.GetComponent<Animator>().SetBool("AllowThrust", false);
            }
            else if (thruster == ThrusterTypes.Right)
            {
                rightThruster.GetComponent<Animator>().SetBool("AllowThrust", false);
            }
        }
    }
}
