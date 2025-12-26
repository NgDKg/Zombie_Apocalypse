using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammunitonText;
    public Text magText;

    public static AmmoCount occurrence;

    private void Awake()
    {
        if (occurrence == null)
        {
            occurrence = this;
        }
    }

    public void UpdateAmmotext(int presentAmmunition)
    {
        ammunitonText.text = "Ammo. " + presentAmmunition;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Magazines. " + mag;
    }
}
