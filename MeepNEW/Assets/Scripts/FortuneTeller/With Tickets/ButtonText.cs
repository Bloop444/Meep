using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonText : MonoBehaviour
{
    public string Tag;
    public float waitTime;
    public ButtonType bt;

    bool waiting;

    TextRandomizer tr;
    TicketCounter tc;

    void Start()
    {
        tr = FindObjectOfType<TextRandomizer>();
        tc = FindObjectOfType<TicketCounter>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag))
        {
            if(!waiting)
            {
                if (bt == ButtonType.Randomizer)
                {
                    if(tc.tickets >= 5)
                    {
                        tc.Buy();
                        tr.RandomizeText();
                    }
                }
                else if (bt == ButtonType.Collect)
                {
                    tc.Collect();
                    Destroy(gameObject);
                }
                StartCoroutine(cd());
            }
        }
    }

    public enum ButtonType
    {
        Randomizer,
        Collect
    }

    IEnumerator cd()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }
}
