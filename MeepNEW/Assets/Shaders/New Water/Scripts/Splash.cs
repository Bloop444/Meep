using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public GameObject splashPrefab;
    public AudioClip splashSound;    
    public string leftControllerName = "LeftHand Controller";
    public string rightControllerName = "RightHand Controller";
    public Transform leftHand;
    public Transform rightHand;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == leftControllerName)
        {
            StartCoroutine(splashyleft());
        }
        else if (other.gameObject.name == rightControllerName)
        {
            StartCoroutine(splashyright());
        }
    }

    public IEnumerator splashyleft()
    {
        source.PlayOneShot(splashSound);
        GameObject splashInstance = Instantiate(splashPrefab, leftHand.position, leftHand.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(splashInstance);
    }

    public IEnumerator splashyright()
    {
        source.PlayOneShot(splashSound);
        GameObject splashInstance = Instantiate(splashPrefab, rightHand.position, rightHand.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(splashInstance);
    }
}
