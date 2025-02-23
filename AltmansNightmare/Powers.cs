/*
*  Author: Jesse Turner
*  Date: 2-10-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    public float powerUpTime = 10f;
    public GameObject leftGun;
    public GameObject rightGun;

   // private bool isFullyAutomatic = false;
    //private bool hasTwoGuns = false;

    private Coroutine fullAutoCoroutine;
    private Coroutine twoGunCoroutine;
    public void FullyAutomatic()
    {
        // Activate the fully automatic power-up
        //isFullyAutomatic = true;
        leftGun.GetComponent<ShootBullet>().isFullAutomatic = true;
        rightGun.GetComponent<ShootBullet>().isFullAutomatic = true;

        // If the full auto coroutine is already running stop it
        if(fullAutoCoroutine != null)
        {
            StopCoroutine(fullAutoCoroutine);
        }

        // Start the full auto coroutine
        fullAutoCoroutine = StartCoroutine(DisableFullyAutomaticAfterTime());
        
    }
    private IEnumerator DisableFullyAutomaticAfterTime()
    {
        // Wait for the specified power-up time
        yield return new WaitForSeconds(powerUpTime);

        // Deactivate fully automatic mode after the time expires
        //isFullyAutomatic = false;
        leftGun.GetComponent<ShootBullet>().isFullAutomatic = false;
        rightGun.GetComponent<ShootBullet>().isFullAutomatic = false;
    }
    public void TwoGuns()
    {
        // Activate the fully automatic power-up
        //hasTwoGuns = true;
        leftGun.SetActive(true);
        if (twoGunCoroutine != null)
        {
            StopCoroutine(twoGunCoroutine);
        }
        twoGunCoroutine = StartCoroutine(DisableTwoGunsAfterTime());
    }
    private IEnumerator DisableTwoGunsAfterTime()
    {
        // Wait for the specified power-up time
        yield return new WaitForSeconds(powerUpTime);

        // Deactivate fully automatic mode after the time expires
        //hasTwoGuns = false;
        leftGun.SetActive(false);
    }
}
