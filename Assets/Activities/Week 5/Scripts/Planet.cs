using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFive
{
    public class Planet : MonoBehaviour
    {
        public float gravityStrength = 5f;
        private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Player>())
            {
                Destroy(collision.transform.gameObject);
            }
        }


        void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb && !affectedRigidbodies.Contains(rb))
            {
                affectedRigidbodies.Add(rb);
            }
        }

        void OnTriggerExit(Collider other)
        {
            Rigidbody rb = other.attachedRigidbody;
            rb.velocity = Vector3.zero;
            if (rb && affectedRigidbodies.Contains(rb))
            {
                affectedRigidbodies.Remove(rb);
            }
        }

        void FixedUpdate()
        {
            ApplyGravityToRigidbodies();
        }

        void ApplyGravityToRigidbodies()
        {
            foreach (var rb in affectedRigidbodies)
            {
                if (rb != null)
                {
                    // get the direction from this planets position to the current rigibodies position.
                    Vector3 gravityDirection = (transform.position - rb.transform.position);
                    // take the gravity direction and normalise it.
                    Vector3 normalisedGravityDirection = gravityDirection.normalized;

                    // lets get the distance between this planet, and the current rigidbody in the loop.
                    float distance = Vector3.Distance(transform.position, rb.transform.position);
                    // lets calculate the gravity by dividing the gravity strength by the distance. 
                    float gravity = gravityStrength / distance;

                    // lets add a force that is the normalised direction multiplied by the gravity.
                    rb.AddForce(normalisedGravityDirection * gravity, ForceMode.Acceleration);
                }
            }
        }
    }
}