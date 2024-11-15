using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedingWall : MonoBehaviour
{
    public float WaitTime = 10f;
    public float AnimationSpeed = 0.50f;
    public Animator Animator;
    public string ClipName;
    private bool StopSpam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StopSpam == false)
        {
            StartCoroutine(StartAnimation());
        }
    }
    IEnumerator StartAnimation()
    {
        StopSpam = true;
        yield return new WaitForSeconds(WaitTime);
        StopSpam = false;
        float Speed = Random.Range(0.25f, AnimationSpeed); 
        Animator.speed = Speed;
        Animator.Play(ClipName);
    }
}
