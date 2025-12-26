using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch var")]
    public Camera cam;
    public float giveDmgOf = 10f;
    public float punchingRange = 5f;

    public void Punch()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie  zombie  = hitInfo.transform.GetComponent<Zombie>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDmg(giveDmgOf);              
            }
            else if (zombie != null)
            {
                zombie.zombieHitDamage(giveDmgOf);     
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDmgOf);
            }
        }
    }
}
