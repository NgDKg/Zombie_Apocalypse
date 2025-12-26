using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("Ammo Boost")]
    public Rifle rifle;
    private int MagBoostAmount = 15;
    private float radius = 2.5f;

    [Header("Sound Effects")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                rifle.mag = MagBoostAmount;

                audioSource.PlayOneShot(AmmoBoostSound);
                Object.Destroy(gameObject, 3.5f);
            }
        }
    }
}

