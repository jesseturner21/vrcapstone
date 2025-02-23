/*
*  Author: Jesse Turner
*  Date: 2-10-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpSpawner: MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public UnityEvent[] powerUpForPrefab;
    public float spawnAreaSize;

    public void SpawnPowerUp(int index)
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnAreaSize;
        GameObject spawnedObject = Instantiate(prefabToSpawn[index], randomPosition, Quaternion.identity);
        //spawnedObject.transform.LookAt(player.transform);
        PowerUp powerUpComponent = spawnedObject.GetComponent<PowerUp>();

        if (powerUpComponent != null)
        {
            powerUpComponent.powerUp.AddListener(powerUpForPrefab[index].Invoke);
        }
    }

  
}
