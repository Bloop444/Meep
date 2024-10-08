using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableObjectsAfterTime : MonoBehaviour
{
    // List of objects to enable
    public List<GameObject> objectsToEnable;

    // List of objects to disable
    public List<GameObject> objectsToDisable;

    // Time in seconds before enabling and disabling the objects
    public float delayTime = 5f;

    void Start()
    {
        // Start the coroutine to enable and disable objects after the delay
        StartCoroutine(EnableDisableObjectsAfterDelay());
    }

    // Coroutine to enable and disable objects after a set amount of time
    IEnumerator EnableDisableObjectsAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Enable each object in the objectsToEnable list
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        // Disable each object in the objectsToDisable list
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
