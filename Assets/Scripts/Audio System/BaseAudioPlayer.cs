using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will be attached to any game object that wants to play audio
/// These sounds will be triggered via an event
/// This script may seem like overkill, but this component can be attached to any game object that
/// wants to play audio while offloading that job to this component
/// </summary>
namespace SuperBrosBros.Audio
{
    public class BaseAudioPlayer : MonoBehaviour
    {
        public AudioSource sfxSource;
        public AudioSource musicSource;
        //Might want to have the sounds on this object
        public List<AudioClip> allSounds = new List<AudioClip>();
        [SerializeField]
        public StringAudioClipDictionary audioDictionary;
        public void PlayMusic(AudioClip musicToPlay)
        {
            //Might want to add a "Fade" method and fade out or just play the clip
            musicSource.clip = musicToPlay;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip sfxToPlay)
        {
            sfxSource.clip = sfxToPlay;
            sfxSource.Play();
        }

        public void FadeOutAudio()
        {
            StartCoroutine(FadeOutAudioRoutine());
        }

        private IEnumerator FadeOutAudioRoutine()
        {
            //Do something..
            yield return new WaitForSeconds(0.1f);
        }
    }
}