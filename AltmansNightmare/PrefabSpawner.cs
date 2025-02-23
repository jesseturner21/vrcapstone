/*
*  Author: Jesse Turner
*  Date: 2-10-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public float spawnTimer = 1;
    public GameObject prefabToSpawn;
    public GameObject player;
    public float spawnAreaSize = 3f;

    public CountDisplay countDisplay;

    private float tempTime;


    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        if(tempTime > spawnTimer)
        {
            SpawnPrefab();
            tempTime = 0;
        }
    }
    public virtual void SpawnPrefab()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnAreaSize;
        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        spawnedObject.GetComponent<ApplyPointOnHit>().countDisplay = countDisplay; // add the count display for the points added when shot
        spawnedObject.transform.LookAt(player.transform); // point the enemy at the player
        if(spawnedObject.CompareTag("Enemy"))spawnedObject.GetComponent<MoveObjectToPlayer>().player = player.transform; // move enemy toward player
    }
}
