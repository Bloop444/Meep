using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitsounds : MonoBehaviour
{
    [Header("This Is The Tag That The Hand Collides With To Make The Sound")]
    public string Tag = "Ex: Wood";
    [Header("This Is To Stop It From Spaming")]
    public float Delay = 0.5f;
    public AudioSource SoundThatPlays;
    bool StopSpam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag)) 
        { 
            if(StopSpam == false)
            {
                StartCoroutine(PlaySound());
            }
        }
    }
    IEnumerator PlaySound()
    {
        StopSpam = true;
        SoundThatPlays.Play();
        yield return new WaitForSeconds(Delay);
        StopSpam = false;
    }
}
