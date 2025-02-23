/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using UnityEngine;

public class StartNumberSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    // Reference to your NumberSpawner component.
    public NumberSpawner numberSpawner;

    // The horizontal gap (in world units) between each number.
    public float gap = 0.5f;

    /// <summary>
    /// Spawns numbers from the numbersToSpawn array,
    /// arranging them horizontally so that the entire row is centered
    /// on the StartSpawner's position with a gap between them.
    /// </summary>
    public void SpawnNumbers(int[] numbersToSpawn)
    {
        if (numberSpawner == null)
        {
            Debug.LogError("NumberSpawner is not assigned.");
            return;
        }

        // Use the StartSpawner's position as the center.
        Vector3 centerPos = transform.position;

        // Calculate the total width needed for the row.
        float totalWidth = (numbersToSpawn.Length - 1) * gap;

        // Loop through each number and spawn it with the proper offset.
        for (int i = 0; i < numbersToSpawn.Length; i++)
        {
            // Calculate offset relative to the center.
            // When i = 0, we start at -totalWidth/2. This centers all the numbers with equal distance on each side
            float offsetX = i * gap - totalWidth / 2f;

            // The spawn position is the center plus the offset (only on the x-axis).
            Vector3 spawnPos = centerPos + new Vector3(offsetX, 0, 0);

            // Spawn the number using the NumberSpawner.
            numberSpawner.SpawnNumber(numbersToSpawn[i], spawnPos);
        }
    }
}
