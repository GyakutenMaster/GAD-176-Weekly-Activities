using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekOne.Completed
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
        [SerializeField] private float smoothSpeed = 0.5f;  // Adjust this value to control the smoothness of the follow

        void LateUpdate()
        {
            UpdateCameraPosition();
        }

        void UpdateCameraPosition()
        {        
            // first check to see if we have a reference to the player and it's not null
            if (player != null)
            {              
                // Calculate the target position for the camera, this will be the players current position, plus the offset.
                Vector3 targetPosition = player.position + offset;

                // Use Lerp to smoothly interpolate between the current position and the target position
                // so we'll get the current step, by lerping from the current position of the camera, to the target position, by our smoothspeed and delta time.
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

                // Set the transform of this camera's position to the smoothed position
                transform.position = smoothedPosition;
            }
        }
    }
}
