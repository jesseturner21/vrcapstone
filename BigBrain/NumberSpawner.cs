/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    [Header("Prefabs and Settings")]
    public GameObject numberContainerPrefab;
    public GameObject[] digitPrefabs;
    public GameObject minusSignPrefab;
    public float digitSpacing = 0.5f;
    public Transform playerTransform;

    // Operation set in the spawner. (Reusing the enum defined in NumberObject.)
    public NumberObject.Operation selectedOperation = NumberObject.Operation.Add;

    // Event that is triggered when a number is spawned.
    public event Action<int> OnNumberSpawned;

    /// <summary>
    /// Executes the selected operation on two numbers.
    /// The first parameter is treated as the "left" value and the second as the "right" value.
    /// For subtraction and division, ordering matters.
    /// </summary>
    public int ExecuteOperation(int left, int right)
    {
        int result = 0;
        switch (selectedOperation)
        {
            case NumberObject.Operation.Add:
                result = left + right;
                break;
            case NumberObject.Operation.Subtract:
                result = left - right;
                break;
            case NumberObject.Operation.Multiply:
                result = left * right;
                break;
            case NumberObject.Operation.Divide:
                result = right != 0 ? left / right : 0;
                break;
        }
        return result;
    }

    /// <summary>
    /// Spawns a new number object by assembling digit prefabs.
    /// </summary>
    public GameObject SpawnNumber(int number, Vector3 position)
    {
        GameObject container = Instantiate(numberContainerPrefab, position, Quaternion.identity);
        NumberObject numObj = container.GetComponent<NumberObject>();
        if (numObj != null)
        {
            numObj.numberValue = number;
            numObj.spawner = GetComponent<NumberSpawner>();
            container.GetComponent<MoveTowardsPlayer>().playerTransform = playerTransform;
        }
        container.transform.LookAt(playerTransform.position);
        container.transform.Rotate(0, 180, 0);

        string numberStr = number.ToString();
        float totalWidth = 0f;
        Vector3 startPos = Vector3.zero;

        if (number < 0)
        {
            GameObject minusSignObj = Instantiate(minusSignPrefab, container.transform);
            minusSignObj.transform.localPosition = startPos;
            startPos = new Vector3(totalWidth, 0, 0);
        }

        totalWidth += (numberStr.Length * digitSpacing);
        startPos.x -= totalWidth / 2;
        for (int i = 0; i < numberStr.Length; i++)
        {
            if (numberStr[i] == '-') continue;
            int digit = int.Parse(numberStr[i].ToString());
            GameObject digitObj = Instantiate(digitPrefabs[digit], container.transform);
            digitObj.transform.localPosition = startPos + new Vector3(i * digitSpacing, 0, 0);
        }

        // Check if the spawned number is the answer
        OnNumberSpawned?.Invoke(number);

        return container;
    }
}
