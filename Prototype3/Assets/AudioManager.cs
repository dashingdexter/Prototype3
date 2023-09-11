using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    static AudioManager current;

    public AudioClip musicClip;
    public AudioClip ambienceClip;
    public AudioClip[] pickupClips;
    public AudioClip[] irondestroyClips;

    AudioSource musicSource;
    AudioSource ambienceSource;
    AudioSource pickupSource;
    AudioSource destroySource;




    private void Awake()
    {
        current = this;

        DontDestroyOnLoad(gameObject);

        ambienceSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        pickupSource = gameObject.AddComponent<AudioSource>();
        destroySource = gameObject.AddComponent<AudioSource>();

        StartLevelAudio();
    }

    void StartLevelAudio()
    {
        current.ambienceSource.clip = current.ambienceClip;
        current.ambienceSource.loop = true;
        current.ambienceSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
        musicSource.volume = 0.3f;

    }

   
    public static void PlayironDestroyAudio()
    {
        int index = Random.Range(0, current.irondestroyClips.Length);

        current.destroySource.clip = current.irondestroyClips[index];
        current.destroySource.Play();

    }

    public static void PlaypickupAudio()
    {
        int index = Random.Range(0, current.pickupClips.Length);

        current.pickupSource.clip = current.pickupClips[index];
        current.pickupSource.Play();

    }




}
