using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourableCosmetics : MonoBehaviour
{
    public GameObject ParticalSystem;
    public float UpdateTime = -0.25f;
    public AudioSource PouringSound;

    private void Update()
    {
        if (IsUpside())
        {
            PouringSound.Play();
            ParticalSystem.SetActive(true);
        }
        else
        {
            PouringSound.Stop();
            ParticalSystem.SetActive(false);
        }
    }

    private bool IsUpside()
    {
        return Vector3.Dot(transform.up, Vector3.down) > UpdateTime;
    }
}
