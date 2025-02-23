/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using UnityEngine;

public class AnswerDisplay : MonoBehaviour
{
    public GameObject[] digitPrefabs;
    public float digitSpacing = 10f; // The spacing between digits

    public Transform centerTransform;


    // Method to update the count and display it
    public void UpdateCount(int newCount)
    {
        // Destroy any previously instantiated digits
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        DisplayCount(newCount);
    }

    // Method to display the count with the digits centered
    public void DisplayCount(int count)
    {
        string countString = count.ToString(); // Convert the count to a string so we can break it into individual digits


        // Calculate the total width of all the digits
        float totalWidth = countString.Length * digitSpacing;

        // Adjust the starting position so the digits are centered
        Vector3 startPosition = centerTransform.position - new Vector3(totalWidth / 2, 0, 0);

        for (int i = 0; i < countString.Length; i++)
        {
            // Get the digit character at this position in the string
            char digitChar = countString[i];
            int digit = digitChar - '0'; // Convert the character to an integer (e.g., '3' -> 3)

            // Instantiate the prefab for this digit at the correct position
            Vector3 position = startPosition + new Vector3(i * digitSpacing, 0, 0); // Adjust position for each digit

            // Instantiate the digit prefab and set it as a child of this object
            Instantiate(digitPrefabs[digit], position, Quaternion.identity, transform);
        }
    }
}
