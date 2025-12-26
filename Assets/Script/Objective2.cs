using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective2 : MonoBehaviour
{
    public GameObject Boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectiveComplete.occurence.GetObjectivesDone(true, true, false, false);
            Boss.SetActive(true);
            Destroy(gameObject, 2f);
        }
    }
}
