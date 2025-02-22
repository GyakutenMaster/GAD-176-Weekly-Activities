using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GAD176.WeeklyActivities.WeekTwo.Completed
{
    public class Lander : MonoBehaviour
    {
        [SerializeField] private Thrusters thrusters; // Reference to our thruster class
        private Rigidbody2D m_Rigidbody; // reference to our rigidbody
        private HingeJoint2D m_feetHingeJoint; //a reference to our hinge joint
        private Transform m_landingPad; // a refernce our goal
        private Vector2 hingeStartingPoint;// the start position of our hinge

        public GameObject explosionPrefab; // The prefab to spawn in when we explode.
        private LunarLanderUIManager uiManager;
        private AudioSource m_audioSource;// a reference to our audio source

        private float currentVelocity = 0;
        [SerializeField] private float landingDistanceThreshold = 1f;
        [SerializeField] private float maxFeetAnchorY = 0.38f;
        [SerializeField] private float feetMovementSpeed = 1f / 3f;
        private float previousVelocity;
        /// <summary>
        /// Returns our current fuel
        /// </summary>
        public float CurrentFuel
        {
            get
            {
                return thrusters.CurrentFuel;
            }
            set
            {
                thrusters.CurrentFuel = value;
            }
        }

        /// <summary>
        /// Returns the current velocity of our rigidbody
        /// </summary>
        public float CurrentVelocity
        {
            get
            {
                // I'll also update the ui for this as well.
                return currentVelocity;
            }
            set
            {
                currentVelocity = value;
                // here we need to devide the current velocity by two to ensure the slider updates correctly.
                uiManager.InGameUI.UpdateVelocity(currentVelocity / 2);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_feetHingeJoint = GetComponentInChildren<HingeJoint2D>(); // reference to our feet hinge joint
            m_landingPad = FindObjectOfType<LandingPad>().transform;//finds the landing pad in our scene and stores the transform
            hingeStartingPoint = m_feetHingeJoint.anchor;
            m_audioSource = GetComponent<AudioSource>();
            uiManager = FindObjectOfType<LunarLanderUIManager>();
            thrusters.SetUp(uiManager);
        }

        // Update is called once per frame
        void Update()
        {
            SetVelocity();
            CheckLandingDistance();
        }



        /// <summary>
        /// Fixed update is what we should be using when we handle physics calculations as it is fixed intervals.
        /// </summary>
        private void FixedUpdate()
        {
            HandleInput();
            PlayThrusterSounds();
        }

        private void SetVelocity()
        {
            // set our previousVelocity to our current velocity.
            previousVelocity = CurrentVelocity;
            // set our CurrentVelocity to the magnitude rigibody's velocity
            CurrentVelocity = m_Rigidbody.velocity.magnitude;
        }

        private void CheckLandingDistance()
        {
            // check to see if the distance between the landers position and the landing pad's position is less then the landing distance threshold.
            if (Vector2.Distance(transform.position, m_landingPad.position) <= landingDistanceThreshold)
            {
                // We are close enough to deploy our feet.
                if (m_feetHingeJoint.anchor.y + (Time.deltaTime * feetMovementSpeed) < maxFeetAnchorY)
                {
                    m_feetHingeJoint.anchor = new Vector2(0f, m_feetHingeJoint.anchor.y + (Time.deltaTime * feetMovementSpeed));
                }
            }
            else
            {
                // We are too far away hide our feet.
                m_feetHingeJoint.anchor = new Vector2(0f, hingeStartingPoint.y + (Time.deltaTime * feetMovementSpeed));
            }
        }

        private void HandleInput()
        {
            thrusters.Thrusting = false;
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");
            UpdateThrusterAnimations(verticalAxis, horizontalAxis);
        }

        void UpdateThrusterAnimations(float verticalInput, float horizontalInput)
        {
            // Another way to get input / the better way or more configurable way
            if (verticalInput > 0)
            {
                // We are pressing the up arrow/W
                thrusters.AddThrust(m_Rigidbody, Thrusters.ThrusterTypes.Main);
            }
            else
            {
                // We don't want to do anything
                thrusters.CancelAnimation(Thrusters.ThrusterTypes.Main);
            }

            // Right arrow input
            if (horizontalInput > 0)
            {
                // left arrow/a
                thrusters.AddThrust(m_Rigidbody, Thrusters.ThrusterTypes.Left);
            }
            else
            {
                // do nothing
                thrusters.CancelAnimation(Thrusters.ThrusterTypes.Left);
            }

            // Left Arrow Input
            if (horizontalInput < 0)
            {
                // Right arrow/D
                thrusters.AddThrust(m_Rigidbody, Thrusters.ThrusterTypes.Right);
            }
            else
            {
                // Do Nothin
                thrusters.CancelAnimation(Thrusters.ThrusterTypes.Right);
            }
        }

        private void PlayThrusterSounds()
        {
            if (thrusters.Thrusting && !m_audioSource.isPlaying)
            {
                m_audioSource.Play();
            }
            else if (!thrusters.Thrusting) // if not applying thrust
            {
                m_audioSource.Stop();
            }
        }

        /// <summary>
        /// Used for collision detection
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // access the collision, and check it's relative velocity, if the length of the vector is greater than 1 blow up the lander
            if (IsFallingTooFast())
            {
                ExplodeLander();
            }
        }

        public bool IsFallingTooFast()
        {
            return previousVelocity > 1;
        }

        /// <summary>
        /// Is called when the lander explodes
        /// </summary>
        private void ExplodeLander()
        {
            GameObject clone = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            Invoke("ReloadCurrentScene", 3);


            Destroy(clone, 0.5f);
            TurnOffAllRenders();
        }

        private void TurnOffAllRenders()
        {
            SpriteRenderer[] allRenders = transform.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < allRenders.Length; i++)
            {
                allRenders[i].enabled = false;
            }
        }

        void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}