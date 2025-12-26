using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Foot Step Sound")]
    public AudioClip[] footStepSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomFootStepSound()
    {
        return footStepSounds[UnityEngine.Random.Range(0, footStepSounds.Length)];
    }

    private void Step()
    {
        AudioClip clip = GetRandomFootStepSound();
        audioSource.PlayOneShot(clip);
    }

}
