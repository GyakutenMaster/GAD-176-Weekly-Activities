using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTen.Completed
{
    /// <summary>
    /// Controls the camera to follow and zoom based on the positions of tanks in the scene.
    /// </summary>
    public class TankCameraController : MonoBehaviour
    {
        [SerializeField] protected float dampTime = 0.2f; // Time it takes for the camera to focus on tanks.
        [SerializeField] protected float screenEdgeBuffer = 4; // Space between the top and bottom of targets.
        [SerializeField] protected float minCameraSize = 6.5f; // The smallest orthographic camera size.
        protected List<GameObject> listOfTanks = new List<GameObject>(); // Reference to all tanks in the scene.

        protected Camera cam; // Reference to the main camera.
        protected float zoomSpeed; // The speed of zooming in/out.
        protected Vector3 moveVelocity; // Velocity for smooth damping position.
        protected Vector3 desiredPosition; // The position the camera should move towards.

        [SerializeField] protected bool enableCameraOnStart = false;

        private void OnEnable()
        {
            cam = GetComponentInChildren<Camera>();
            // scubscribe our OnGameStart Function to our SpawnPlayerEvent
            GameEvents.SpawnPlayerEvent += OnGameStart;
        }

        private void OnDisable()
        {
            // Additional cleanup can be done here if needed.
            GameEvents.SpawnPlayerEvent -= OnGameStart;
            // unscubscribe our OnGameStart Function to our SpawnPlayerEvent
            GameEvents.SpawnPlayerEvent -= OnGameStart;
        }

        private void Start()
        {
            // Used if we are not using the spawn tank event.
            if (enableCameraOnStart)
            {
                OnGameStart();
            }
        }

        // Update is called once per frame
        void Update()
        {
            Move(); // Move the camera.
            Zoom(); // Zoom in to show all tanks.
        }

        void OnGameStart(int numberOfPlayers = 0)
        {
            Initialize(FindAllTanks());
        }

        private List<GameObject> FindAllTanks()
        {
            Tank[] allTanks = FindObjectsOfType<Tank>();
            List<GameObject> allTanksList = new List<GameObject>();

            for (int i = 0; i < allTanks.Length; i++)
            {
                allTanksList.Add(allTanks[i].gameObject);
            }
            return allTanksList;
        }

        /// <summary>
        /// Called when the tanks have been spawned in and not beforehand.
        /// </summary>
        private void Initialize(List<GameObject> allTanks)
        {
            listOfTanks = allTanks;

            FindAveragePosition();
            transform.position = desiredPosition;
            cam.orthographicSize = CalculateRequiredSize();
        }

        /// <summary>
        /// Moves the camera to the desired position.
        /// </summary>
        private void Move()
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
        }

        /// <summary>
        /// Handles the zooming in and out of the camera.
        /// </summary>
        private void Zoom()
        {
            float requiredSize = CalculateRequiredSize();
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
        }

        /// <summary>
        /// Calculates the required size of the camera to include all tanks.
        /// </summary>
        /// <returns>The required size of the camera.</returns>
        private float CalculateRequiredSize()
        {
            Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);

            float size = 0;

            for (int i = 0; i < listOfTanks.Count; i++)
            {
                if (listOfTanks[i].activeSelf == false)
                {
                    continue;
                }
                else
                {
                    Vector3 targetLocalPos = transform.InverseTransformPoint(listOfTanks[i].transform.position);
                    Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPosition;

                    size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                    size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
                }
            }

            size += screenEdgeBuffer;
            size = Mathf.Max(size, minCameraSize);

            return size;
        }

        /// <summary>
        /// Loops through the tanks and finds their positions to calculate the average.
        /// </summary>
        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            for (int i = 0; i < listOfTanks.Count; i++)
            {
                if (listOfTanks[i].activeSelf == false)
                {
                    continue;
                }
                else
                {
                    averagePos += listOfTanks[i].transform.position;
                    numTargets++;
                }
            }

            if (numTargets > 0)
            {
                averagePos /= numTargets;
            }

            averagePos.y = transform.position.y;
            desiredPosition = averagePos;
        }
    }
}
