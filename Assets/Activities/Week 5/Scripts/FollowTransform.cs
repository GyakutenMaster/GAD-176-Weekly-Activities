using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFive
{
    public class FollowTransform : MonoBehaviour
    {

        [SerializeField] private float cameraZDepth = -10f; //this is the distance the camera will be from the player.

        [SerializeField] private float trackingSpeed;// the speed the tracks/follows the player

        private Transform transformToFollow;// the transform the camera is following.
        private Vector3 cameraPosition; // The position of the camera at this moment.


        private void Start()
        {
            // search the scene for the lander script, then access it's transform and assign it to the transform
            transformToFollow = FindObjectOfType<Player>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (transformToFollow == null)
            {
                return;
            }

            Track();
        }

        /// <summary>
        /// Applies movement to the camera
        /// </summary>
        private void Track()
        {
            // Lerp the current postion, to the tranform to follows position, by the tracking speed and smooth it by Time.deltaTime
            cameraPosition = Vector2.Lerp(transform.position, transformToFollow.position, Time.deltaTime * trackingSpeed);

            // Set the current position, to the lerp position we just calculated above, however only on the x and y, for z we want to use cameraZDepth
            transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraZDepth);

        }
    }
}
