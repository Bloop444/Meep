using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using easyInputs;

public class JumpscareManager : MonoBehaviour
{
    public EasyHand Hand = EasyHand.RightHand;
    public float JumpscareGunDuration = 3f;
    public GameObject JumpscareObject;
    public AudioSource JumpscareSound;
    internal bool jumpscaring;
    private bool stopspam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpscaring == true && stopspam == false)
        {
            StartCoroutine(StartJumpscare());
        }
    }
    IEnumerator StartJumpscare()
    {
        JumpscareObject.SetActive(true);
        stopspam = true;
        JumpscareSound.Play();
        yield return new WaitForSeconds(JumpscareGunDuration);
        JumpscareSound.Stop();
        stopspam = false;
        jumpscaring = false;
        JumpscareObject.SetActive(false);
    }
}
