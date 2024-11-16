using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedingWall : MonoBehaviour
{
    public Material RegularMaterial;
    public Material AngryMaterial;
    public Renderer WallRender;
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
        float TimeWaitTime = Random.Range(5, WaitTime);
        StopSpam = true;
        WallRender.material = RegularMaterial;
        yield return new WaitForSeconds(TimeWaitTime);
        WallRender.material = AngryMaterial;
        float Speed = Random.Range(0.25f, AnimationSpeed);
        Animator.speed = Speed;
        Animator.Play(ClipName);
        yield return new WaitForSeconds(TimeWaitTime);
        StopSpam = false;
    }
}
