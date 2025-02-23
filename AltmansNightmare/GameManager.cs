/*
*  Author: Jesse Turner
*  Date: 2-10-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float waveTime;
    public float powerUpFrequency;
    public PowerUpSpawner powerUpSpawner;
    public CountDisplay countDisplay;

    private int powerUpsGiven = 0;
    private float time;
    private GameObject[] waves;
    private int index = 0;

    private void Start()
    {
        waves = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            waves[i] = transform.GetChild(i).gameObject;
        }
        NextWave();
    }
    private void Update()
    {
        time += Time.deltaTime;

        if (time > waveTime)
        {
            NextWave();
            time = 0;
        }

        if(countDisplay.count / powerUpFrequency > powerUpsGiven)
        {
            int powerUpSelected = Random.Range(0, powerUpSpawner.powerUpForPrefab.Length);
            powerUpSpawner.SpawnPowerUp(powerUpSelected);
            powerUpsGiven += 1;
        }
    }
    private void NextWave()
    {
        if(index < waves.Length )
        {
            // enable another wave
            waves[index].SetActive(true);
            index += 1;
        }
    }
    public static void EndGame()
    {

        SceneManager.LoadScene("EndScene");
    }
}
