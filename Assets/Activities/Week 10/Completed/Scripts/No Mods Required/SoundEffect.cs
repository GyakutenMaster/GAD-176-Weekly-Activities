using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTen.Completed
{
    /// <summary>
    /// ScriptableObject representing a sound effect.
    /// </summary>
    [CreateAssetMenu(fileName = "Week 10", menuName = "Completed/Week 10/SoundEffect", order = 0)]
    public class SoundEffect : ScriptableObject
    {
        [SerializeField] protected AudioClip soundEffect; // the tank idling clip
        [SerializeField] protected float pitchRangeMax = 0.2f; // the maximum amount our pitch can be changed by
        protected float originalPitchLevel; // the starting pitch level before we modify it 
        protected AudioSource instancedAudioSource = null;

        /// <summary>
        /// Plays the sound effect using the provided AudioSource or creates a new one.
        /// </summary>
        /// <param name="sourceToPlay">Optional AudioSource to use for playing the sound.</param>
        public void PlaySound(AudioSource sourceToPlay = null)
        {
            instancedAudioSource = sourceToPlay;

            if (instancedAudioSource == null)
            {
                // Create a new game object and destroy it after use.
                instancedAudioSource = new GameObject().AddComponent<AudioSource>();
                Destroy(instancedAudioSource.gameObject, soundEffect.length);
            }

            // If the AudioSource is already playing the same sound effect, do nothing.
            if (instancedAudioSource.clip == soundEffect && instancedAudioSource.isPlaying)
            {
                return;
            }

            originalPitchLevel = instancedAudioSource.pitch; // set the starting pitch

            // Set up the AudioSource for the idle sound
            if (instancedAudioSource)
            {
                instancedAudioSource.clip = soundEffect; // set the audio to our idle sound
                instancedAudioSource.pitch = Random.Range(originalPitchLevel - pitchRangeMax, originalPitchLevel + pitchRangeMax); // get a random pitch level
                instancedAudioSource.Play(); // play our new clip
            }
        }

        /// <summary>
        /// Stops the currently playing sound effect.
        /// </summary>
        public void StopSound()
        {
            if (instancedAudioSource != null)
            {
                instancedAudioSource.Stop();
            }
        }
    }
}