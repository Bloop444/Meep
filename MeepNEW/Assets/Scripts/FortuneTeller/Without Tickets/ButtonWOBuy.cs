using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWOBuy : MonoBehaviour
{
    public string Tag;

    TextRandomizer tr;

    void Start()
    {
        tr = FindObjectOfType<TextRandomizer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            tr.RandomizeText();
        }
    }
}
