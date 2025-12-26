using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle things")]
    public Camera cam;
    public float giveDmgOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    public Animator animator;
    public Player player;
    public Transform hand;
    private float nextTimeToShoot = 0f;
    public GameObject rifleUI;


    [Header("Rifle Ammunition and shooting")]
    private int maximumAmmunition = 32;
    public int mag = 10;
    private int presentAmmuniton;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    [Header(("Show and UI"))]
    public GameObject ammoOutUI;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;


    [Header("Rifle effect")]
    public ParticleSystem muzzleSpark;
    public GameObject woodEffect;
    public GameObject goreEffect;


    private void Awake()
    {
        transform.SetParent(hand);

        rifleUI.SetActive(true);
        presentAmmuniton = maximumAmmunition;
        
    }
    private void FixedUpdate()
    {       
        if(setReloading)
        {
            return;
        }
        if(presentAmmuniton <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f/fireCharge;
            Shoot();
        }
        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }  
    private void Shoot()
    {
        if(mag == 0)
        {
            StartCoroutine(ShowAmmoOut());
            return;
        }

        presentAmmuniton --;

        if(presentAmmuniton == 0)
        {
            mag--;
        }

        AmmoCount.occurrence.UpdateAmmotext(presentAmmuniton);
        AmmoCount.occurrence.UpdateMagText(mag);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {   
            Debug.Log(hitInfo.transform.name);
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie zombie   = hitInfo.transform.GetComponent<Zombie>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDmg(giveDmgOf);
                GameObject WoodGo = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if (zombie != null)
            {
                zombie.zombieHitDamage(giveDmgOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDmgOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading  . . .");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmuniton = maximumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;
    }

    IEnumerator ShowAmmoOut()
    {
        ammoOutUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        ammoOutUI.SetActive(false);
    }
}
