using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekFour
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float bulletSpeed = 10;

        // Start is called before the first frame update
        void Start()
        {
            if(GetComponent<Rigidbody>())
            {
                // lets set the rigidbodies velocity to be the up world vector multiplied by the bullet speed.
                GetComponent<Rigidbody>().velocity = Vector3.up * bulletSpeed;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<Asteroid>())
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
