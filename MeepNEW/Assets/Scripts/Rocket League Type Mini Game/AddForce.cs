using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public string HandTag = "HandTag";
    public float ForceMultiplier = 10f;
    private Rigidbody BallRB;
    // Start is called before the first frame update
    void Start()
    {
        BallRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag(HandTag))
        {
            Vector3 ImpactDirection = other.transform.position - transform.position;
            ImpactDirection.Normalize();
            BallRB.AddForce(ImpactDirection * ForceMultiplier, ForceMode.Impulse);
        }
    }
}
