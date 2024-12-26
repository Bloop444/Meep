using UnityEngine;

public class ParticleOnMovement : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private Vector3 lastPosition;
    private bool isMoving;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if the object has moved
        isMoving = transform.position != lastPosition;
        lastPosition = transform.position;

        // Toggle particle emission based on movement
        var emission = particleSystem.emission;
        emission.enabled = isMoving;
    }
}
