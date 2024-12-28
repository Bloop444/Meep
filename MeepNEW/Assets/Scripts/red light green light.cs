using System.Collections;
using UnityEngine;

public class RedLightGreenLight : MonoBehaviour
{
    public float timer = 30f;
    public Material greenLightMaterial;
    public Material redLightMaterial;
    public GameObject lightObject; // The object that represents the light
    public Transform respawnPoint;
    public Transform finishLine;

    private bool isGreenLight = true;
    private bool gameStarted = false;
    private GameObject player;

    void Start()
    {
        // Initialize the light to green
        SetLight(true);
    }

    void Update()
    {
        if (gameStarted && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame();
            }
        }

        if (player != null && !PlayerCrossedFinishLine())
        {
            if (!isGreenLight && PlayerMoved())
            {
                RespawnPlayer();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameStarted)
        {
            player = other.gameObject;
            gameStarted = true;
            StartCoroutine(SwitchLights());
        }
    }

    private void SetLight(bool green)
    {
        isGreenLight = green;
        Renderer renderer = lightObject.GetComponent<Renderer>();
        renderer.material = green ? greenLightMaterial : redLightMaterial;
    }

    private bool PlayerMoved()
    {
        // Check if the player moved by comparing their velocity to a small threshold
        Rigidbody rb = player.GetComponent<Rigidbody>();
        return rb.velocity.magnitude > 0.1f;
    }

    private void RespawnPlayer()
    {
        player.transform.position = respawnPoint.position;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    private bool PlayerCrossedFinishLine()
    {
        return player.transform.position.z >= finishLine.position.z;
    }

    private IEnumerator SwitchLights()
    {
        while (timer > 0 && !PlayerCrossedFinishLine())
        {
            SetLight(false);
            yield return new WaitForSeconds(Random.Range(3, 6)); // Red light duration
            SetLight(true);
            yield return new WaitForSeconds(Random.Range(3, 6)); // Green light duration
        }
    }

    private void EndGame()
    {
        StopAllCoroutines();
        // Additional logic for when the game ends (e.g., display a message)
    }
}
