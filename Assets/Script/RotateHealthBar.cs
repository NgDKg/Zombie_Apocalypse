using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBar : MonoBehaviour
{
    public Transform MainCamera;

    public void LateUpdate()
    {
        transform.LookAt(transform.position + MainCamera.forward);
    }

}
