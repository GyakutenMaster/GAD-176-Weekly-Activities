using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEleven.Completed
{
    [CreateAssetMenu(fileName = "Week 11", menuName = "Completed/Week 11/Particles", order = 0)]
    public class ParticleEffects : ScriptableObject
    {
        [SerializeField] protected GameObject[] allParticles = new GameObject[] { }; // Array to store all particle effects.
        protected List<ParticleSystem> instancedParticles = new List<ParticleSystem>();
        protected Transform parentObject;

        /// <summary>
        /// Sets up particle effects based on the provided parent object and plays them on spawn if specified.
        /// </summary>
        /// <param name="parentObject">Transform of the parent object.</param>
        /// <param name="playOnSpawn">Flag to determine whether to play effects on spawn.</param>
        public void SetUpEffects(Transform ParentObject, bool playOnSpawn)
        {
            parentObject = ParentObject;

            // Instantiate particle effects and add their ParticleSystem components to the list.
            for (int i = 0; i < allParticles.Length; i++)
            {
                GameObject clone = Instantiate(allParticles[i], parentObject);
                instancedParticles.AddRange(clone.GetComponentsInChildren<ParticleSystem>());
            }

            // Play effects if specified.
            PlayEffects(playOnSpawn);
        }

        /// <summary>
        /// Deinitializes the particle effects.
        /// </summary>
        public void DeInit()
        {
            // Destroy all instantiated particle systems.
            for (int i = 0; i < instancedParticles.Count; i++)
            {
                Destroy(instancedParticles[i]);
            }
        }

        /// <summary>
        /// Plays or stops the particle effects based on the enabled parameter.
        /// </summary>
        /// <param name="enabled">Flag to determine whether to play or stop the effects.</param>
        public void PlayEffects(bool enabled)
        {
            // Loop through all instanced particle systems.
            for (int i = 0; i < instancedParticles.Count; i++)
            {
                if (enabled)
                {
                    // Play the particle effect.
                    instancedParticles[i].Play();
                }
                else
                {
                    // Stop the particle effect.
                    instancedParticles[i].Stop();
                }
            }
        }
    }
}
