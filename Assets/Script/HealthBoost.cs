using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBoost : MonoBehaviour
{
    [Header("Health Boost")]
    public Player player;
    private float healthBoostAmount = 120f;
    private float radius = 2.5f;

    [Header("Sound Effects")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;

    public HealthBar healthBar;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                player.presentHealth = healthBoostAmount;
                healthBar.SetHealth(player.presentHealth);
                audioSource.PlayOneShot(HealthBoostSound);
                Object.Destroy(gameObject, 3.5f);
            }
        }
    }
}
