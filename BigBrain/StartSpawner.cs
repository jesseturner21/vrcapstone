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
    /// on the NumberSpawner's position with a gap of 0.5 units between them.
    /// </summary>
    public void SpawnNumbers(int[] numbersToSpawn)
    {
        if (numberSpawner == null)
        {
            Debug.LogError("NumberSpawner is not assigned.");
            return;
        }

        // Use the NumberSpawner's position as the center.
        Vector3 centerPos = transform.position;

        // Calculate the total width needed for the row.
        // (For n items, the distance between the first and last is (n-1)*gap.)
        float totalWidth = (numbersToSpawn.Length - 1) * gap;

        // Loop through each number and spawn it with the proper offset.
        for (int i = 0; i < numbersToSpawn.Length; i++)
        {
            // Calculate offset relative to the center.
            // When i = 0, we start at -totalWidth/2.
            float offsetX = i * gap - totalWidth / 2f;

            // The spawn position is the center plus the offset (only on the x-axis).
            Vector3 spawnPos = centerPos + new Vector3(offsetX, 0, 0);

            // Spawn the number using the NumberSpawner.
            // This method call should match the signature defined in your NumberSpawner script.
            numberSpawner.SpawnNumber(numbersToSpawn[i], spawnPos);
        }
    }
}
