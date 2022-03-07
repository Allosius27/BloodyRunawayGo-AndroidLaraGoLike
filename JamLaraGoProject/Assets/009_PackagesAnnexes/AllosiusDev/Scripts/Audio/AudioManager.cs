//
// Updated by Allosius(Yanis Q.) on 17/10/2021.
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AllosiusDev {
    [AddComponentMenu("Audio/Audio Manager")]
    public class AudioManager : Singleton<AudioManager> {
        [SerializeField] private AudioMixerGroup outputSfx, outputMusics, outputAmbients;

        /// <summary>Play a Sound in 2D.</summary>
        /// <param name="sound">The Sound to play</param>
        public static AudioSource Play(Sound sound) {
            return Play(sound, null);
        }

        /// <summary>Play a Sound in 3D.</summary>
        /// <param name="sound">Sound to play.</param>
        /// <param name="position">The position to play the Sound at.</param>
        public static AudioSource Play(Sound sound, Vector3 position) {
            return Play(sound, position, null);
        }

        /// <summary>Play a Sound in 3D.</summary>
        /// <param name="sound">The Sound to play.</param>
        /// <param name="transform">The Transform to attach the AudioSource to.</param>
        public static AudioSource Play(Sound sound, Transform transform) {
            return Play(sound, Vector3.zero, transform);
        }

        private static AudioSource Play(Sound sound, Vector3 position, Transform transform) {
            AudioSource source = FindFreeAudiosource(Instance.audioSources);
            source.transform.parent = transform ?? Instance.transform;
            source.transform.position = transform == null ? position : transform.position;

            //source.outputAudioMixerGroup = sound.Parameters.Is2D ? Instance.outputMusics : Instance.outputSfx;
            if (sound.Parameters.typeSound == SoundParameters.TypeSound.Music)
            {
                source.outputAudioMixerGroup = Instance.outputMusics;
            }
            else if (sound.Parameters.typeSound == SoundParameters.TypeSound.Sfx)
            {
                source.outputAudioMixerGroup = Instance.outputSfx;
            }
            else if (sound.Parameters.typeSound == SoundParameters.TypeSound.Ambients)
            {
                source.outputAudioMixerGroup = Instance.outputAmbients;
            }

            source.SetSoundToSource(sound);
            source.Play();
            return source;
        }

        public static void Stop(Sound sound)
        {
            foreach(AudioSource audioSource in Instance.audioSources)
            {
                if(audioSource.clip == sound.Clip)
                {
                    audioSource.Stop();
                    return;
                }
            }

        }

        public static void StopAllMusics()
        {
            foreach(AudioSource audioSource in Instance.audioSources)
            {
                if(audioSource.outputAudioMixerGroup == Instance.outputMusics)
                {
                    audioSource.Stop();
                }
            }
        }

        public static void StopAllSfx()
        {
            foreach (AudioSource audioSource in Instance.audioSources)
            {
                if (audioSource.outputAudioMixerGroup == Instance.outputSfx)
                {
                    audioSource.Stop();
                }
            }
        }

        public static void StopAllAmbients()
        {
            foreach (AudioSource audioSource in Instance.audioSources)
            {
                if (audioSource.outputAudioMixerGroup == Instance.outputAmbients)
                {
                    audioSource.Stop();
                }
            }
        }

        public static AudioSource FindFreeAudiosource(List<AudioSource> audioSources) {
            foreach(AudioSource audioSource in audioSources) {
                if(!audioSource.isPlaying) return audioSource;
            }
            return Instance.CreateNewAudioSource();
        }

        private AudioSource CreateNewAudioSource() {
            GameObject go = new GameObject("Audio Source");
            go.transform.parent = transform;
            AudioSource newAudioSource = go.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
            audioSources.Add(newAudioSource);
            return newAudioSource;
        }

        private List<AudioSource> audioSources = new List<AudioSource>();
    }
}