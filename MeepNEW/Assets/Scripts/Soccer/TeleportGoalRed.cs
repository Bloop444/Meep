using UnityEngine;

public class TeleportGoalRed : MonoBehaviour
{
    public Transform teleportTarget; // Reference to the teleport target GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedGoal")) // Check if the collider is tagged as "GoalRed"
        {
            TeleportBall();
        }
    }

    private void TeleportBall()
    {
        if (teleportTarget != null)
        {
            transform.position = teleportTarget.position; // Teleport the ball to the target position
        }
        else
        {
            Debug.LogError("Teleport target is not assigned!"); // Log an error if teleportTarget is not assigned
        }
    }
}
