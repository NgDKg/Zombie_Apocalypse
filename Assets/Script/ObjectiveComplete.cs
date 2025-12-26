using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveComplete : MonoBehaviour
{
    [Header("Objective Complete UI")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    public static ObjectiveComplete occurence;

    //private void Awake()
    //{
    //    occurence = this; 
    //}

    private void Awake()
    {
        if (occurence == null)
            occurence = this;
        else
            Destroy(gameObject);
    }

    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            objective1.text = "01. Complete";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "01. Find the Rifle";
            objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            objective2.text = "02. Complete";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "02. Locate the villagers";
            objective2.color = Color.white;
        }

        if (obj3 == true)
        {
            objective3.text = "03. Complete";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "03. FIND VEHICLE";
            objective3.color = Color.white;
        }

        if (obj4 == true)
        {
            objective4.text = "04. Complete";
            objective4.color = Color.green;
        }
        else
        {
            objective4.text = "04. GET ALL VILLAGERS INTO VEHICLE";
            objective4.color = Color.white;
        }
    }

}
