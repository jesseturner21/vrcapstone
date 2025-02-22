using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int answer = 5;
    public int[] numbersToSpawn;

    [Header("References")]
    public AnswerDisplay answerDisplay;
    public NumberSpawner numberSpawner;
    public StartNumberSpawner startNumberSpawner;
    public SceneTransitionManager sceneTransitionManager;

    private bool levelEnded = false;


 

    void Start()
    {
        // Display the answer on the counter.
        if (answerDisplay != null)
        {
            answerDisplay.DisplayCount(answer);
        }
        else
        {
            Debug.LogError("CountDisplay reference is not set in LevelManager.");
        }

        // Subscribe to the event from the spawner and start spawning numbers.
        if (numberSpawner != null)
        {
            numberSpawner.OnNumberSpawned += HandleNumberSpawned;
        }
        else
        {
            Debug.LogError("NumberSpawner reference is not set in LevelManager.");
        }

        StartCoroutine(WaitAndSpawnNumbers());
    }
    // Use a coroutine to wait for the numbers to spawn
    private IEnumerator WaitAndSpawnNumbers()
    {
        // Wait for 1 second.
        yield return new WaitForSeconds(1f);

        if (startNumberSpawner != null)
        {
            startNumberSpawner.SpawnNumbers(numbersToSpawn);
        }
        else
        {
            Debug.LogError("StartNumberSpawner reference is not set in LevelManager.");
        }
    }

    private void HandleNumberSpawned(int spawnedNumber)
    {
        if (levelEnded)
            return;

        Debug.Log("Spawned number: " + spawnedNumber);

        if (spawnedNumber == answer)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        levelEnded = true;
        Debug.LogError("Answer matched! Ending level...");

        if (numberSpawner != null)
        {
            numberSpawner.OnNumberSpawned -= HandleNumberSpawned;
        }

        NumberObject[] numberObjects = FindObjectsOfType<NumberObject>();
        foreach (NumberObject numberObject in numberObjects)
        {
            Destroy(numberObject.gameObject);
        }

        GetComponent<AudioSource>().Play();
        sceneTransitionManager.GoToScene(0);
    }
}
