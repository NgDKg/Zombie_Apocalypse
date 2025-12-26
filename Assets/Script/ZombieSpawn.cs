using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Spawn Var")]
    public GameObject zombiePrefab;
    public GameObject dangerZone1;
    public Transform zombieSpawnPosition;
    private float repeatCycle = 1f;

    [Header("Audio Var")]
    public AudioSource audioSource;
    public AudioClip dangerZoneSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            audioSource.PlayOneShot(dangerZoneSound);
            StartCoroutine(DangerzoneTimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
              
        }
    }
    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }

    IEnumerator DangerzoneTimer()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone1.SetActive(false );
    }
}
