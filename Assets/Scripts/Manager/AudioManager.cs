using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    void Start()
    {
        PlayBackGroundMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBackGroundMusic()
    {
        backgroundAudioSource.clip = backGroundClip;
        backgroundAudioSource.Play();
    }

    public void PlayCoinSound()
    {
        effectAudioSource.PlayOneShot(coinClip);
    }

    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip);
    }
}
