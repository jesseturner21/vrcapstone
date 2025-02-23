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
        // Increment time by the time passed since the last frame
        time += Time.deltaTime;
    
        // If the time exceeds the wave time, proceed to the next wave and reset time
        if (time > waveTime)
        {
            NextWave();  // Call the NextWave method to start the next wave
            time = 0;    // Reset the time counter to 0 for the next wave
        }
    
        // Check if the count of items divided by the power-up frequency exceeds the number of power-ups given
        if(countDisplay.count / powerUpFrequency > powerUpsGiven)
        {
            // Randomly select a power-up from the available power-ups
            int powerUpSelected = Random.Range(0, powerUpSpawner.powerUpForPrefab.Length);
            
            // Spawn the selected power-up
            powerUpSpawner.SpawnPowerUp(powerUpSelected);
    
            // Increment the count of power-ups given to avoid giving more than the limit
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
