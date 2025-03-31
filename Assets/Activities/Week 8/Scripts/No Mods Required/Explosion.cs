using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAD176.WeeklyActivities.WeekEight
{
    public class Explosion : MonoBehaviour
    {

        [SerializeField] protected float damageAtCentre;
        [SerializeField] protected float explosionRadius;
        [SerializeField] protected float explosionDelay;
        protected float timer = 0;
        
        public void Initiate()
        {
            timer = Time.time + explosionDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if(timer > 0 && Time.time >= timer)
            {
                Explode();
            }
        }

        void Explode()
        {
            Collider[] allCollidersInRange =  Physics.OverlapSphere(transform.position, explosionRadius);

            for (int i = 0; i < allCollidersInRange.Length; i++)
            {
                Enemy enemyHit = allCollidersInRange[i].transform.GetComponent<Enemy>();
                if (enemyHit)
                {
                    // if you wanted it to do damage to a player, you'd probs have a health class that both the player and enemy has.
                    // so you could search for that on the collider.

                    float distance = Vector3.Distance(transform.position, enemyHit.transform.position);
                    float normalizedDistance = 1f - Mathf.Clamp01(distance / explosionRadius);
                    float damage = normalizedDistance * damageAtCentre;
                    enemyHit.ChangeHealth(-damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
