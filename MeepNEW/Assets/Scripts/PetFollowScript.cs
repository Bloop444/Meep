using UnityEngine;

public class PetFollowScript : MonoBehaviour
{
    [Header("Don't change any settings but you can if you want")]
    public Transform gorillaPlayer; // The GameObject to follow (Gorilla Player)
    public float moveSpeed = 3f; // Speed at which this GameObject follows the gorilla player
    public float distanceAwayFromGorillaPlayer = 0.7f; // Distance at which this GameObject stops following
    public float smoothTime = 0.7f; // Smoothing factor for movement

    private Vector3 velocity = Vector3.zero; // Initial velocity for smoothing

    private void Update()
    {
        // Check if the gorilla player is not null
        if (gorillaPlayer != null)
        {
            // Calculate the target position
            Vector3 targetPosition = gorillaPlayer.position + (gorillaPlayer.forward * -distanceAwayFromGorillaPlayer);

            // Smoothly move towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
