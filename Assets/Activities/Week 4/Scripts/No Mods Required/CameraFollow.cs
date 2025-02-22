using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
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
            if (player != null)
            {
                // Calculate the target position for the camera
                Vector3 targetPosition = player.position + offset;

                // Use Lerp to smoothly interpolate between the current position and the target position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

                // Set the camera position to the smoothed position
                transform.position = smoothedPosition;
            }
        }
    }
}
