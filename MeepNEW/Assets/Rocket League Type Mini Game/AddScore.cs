using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public RocketLeagueManager ManagerScript;
    public string BallTag = "Ball";
    [Header("This Is Just If Its Like Football Were Score Goes Up By \n7!")]
    public float AmountToAdd = 1f;
    [Header("This Is If Its The Blues Goal!")]
    public bool IsBlue;
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
        if(other.CompareTag(BallTag))
        {
            if(IsBlue == false)
            {
                ManagerScript.RedScore =+ AmountToAdd;
                ManagerScript.ResetPosition();
            }
            else
            {
                ManagerScript.BlueScore =+ AmountToAdd;
                ManagerScript.ResetPosition();
            }
        }
    }
}
