﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            PlayerController.instance.AddGun(theGun);

            Destroy(gameObject);

            collected = true;

            // våpen pickups
            AudioManager.instance.PlaySFX(4);
        }
    }
}
