using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAD176.WeeklyActivities.WeekSix.Completed
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
            // here let's use a physics overlap sphere from the position of the explosion and the explosion radius.
            Collider[] allCollidersInRange =  Physics.OverlapSphere(transform.position, explosionRadius);

            for (int i = 0; i < allCollidersInRange.Length; i++)
            {
                Enemy enemyHit = allCollidersInRange[i].transform.GetComponent<Enemy>();
                Transform enemyTransform = allCollidersInRange[i].transform;
                if (enemyHit)
                {
                    // if you wanted it to do damage to a player, you'd probs have a health class that both the player and enemy has.
                    // so you could search for that on the collider.

                    // let's calculate the distance from our position and the enemy position.
                    float distance = Vector3.Distance(transform.position, enemyTransform.position);
                    // we need the normalised distance, this is going to 1 minus the distance divided by the explosion radius 
                    // we should also use a Mathf.Clamp01 for the result of the distance divided by the explosion radius.
                    float normalizedDistance = 1f - Mathf.Clamp01(distance / explosionRadius);
                    // the damage will be our normalised distance multiplied by the damage at the centre.
                    float damage = normalizedDistance * damageAtCentre;

                    enemyHit.ChangeHealth(-damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
