using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySoundAudioPlayer(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }
}

