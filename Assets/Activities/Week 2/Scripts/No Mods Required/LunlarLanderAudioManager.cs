using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwo
{
    [RequireComponent(typeof(AudioSource))]
    public class LunlarLanderAudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> explosionSFX = new List<AudioClip>(); // a list of all explosion effects
        [SerializeField] private List<AudioClip> collectableSFX = new List<AudioClip>(); // a list of all our collectable sounds.
        private AudioSource m_AudioSource; // reference to my audio source


        private void OnEnable()
        {
            LunarLanderGameManager.playerDeath.AddListener(ExplosionSFX);
            LunarLanderGameManager.playerPickedUp.AddListener(CollectSFX);
        }

        private void OnDisable()
        {
            LunarLanderGameManager.playerDeath.RemoveListener(ExplosionSFX);
            LunarLanderGameManager.playerPickedUp.RemoveListener(CollectSFX);
        }

        // Start is called before the first frame update
        void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        ///  Plays the explosion effect when the player dies.
        /// </summary>
        private void ExplosionSFX()
        {
            m_AudioSource.PlayOneShot(ReturnRandomAudioClipFromList(explosionSFX));
        }

        /// <summary>
        /// Plays a pick up sound for our fuel
        /// </summary>
        private void CollectSFX()
        {
            m_AudioSource.PlayOneShot(ReturnRandomAudioClipFromList(collectableSFX));
        }

        /// <summary>
        /// Returns a random clip from a list of audio clips
        /// </summary>
        /// <param name="clips"></param>
        /// <returns></returns>
        private AudioClip ReturnRandomAudioClipFromList(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }

    }
}