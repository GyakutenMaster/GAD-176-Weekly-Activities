
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwelve
{

    public class DefaultPoolItem : ObjectPoolItem
    {
        public override void OnDespawn()
        {
            // call the OnDespawn function in the base class.
            base.OnDespawn();

            // set the gameobject to not active.
            gameObject.SetActive(false);

            // search the game object for a rigidbody.
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                // lets set the angular velocity to zero.
                rb.angularVelocity = Vector3.zero;
                // lets set the velocity to zero.
                rb.velocity = Vector3.zero;
            }

            // search this gameobject for an audio source.
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            if (audio)
            {
                // if theres an audio source call stop.
                audio.Stop();
            }

            // use getcomponents in children for this gameobject and search for all the particle systems.
            ParticleSystem[] allParticles = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < allParticles.Length; i++)
            {
                // for each one call stop on the particles.
                allParticles[i].Stop();
            }

        }

        public override void OnSpawn()
        {
            // here lets set the gameobject to be active.
            gameObject.SetActive(true);

            // so this one lets access this gameobject, and lets getComponentsInChildren, search for all the particle system.
            ParticleSystem[] allParticles = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < allParticles.Length; i++)
            {
                // loop through all the particles in the array and call play on each one.
                allParticles[i].Play();
            }

            // search this gameobject for an audiosource component
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            if (audio)
            {
                // if there's an audio source call play on it.
                audio.Play();
            }
        }

    }
}
