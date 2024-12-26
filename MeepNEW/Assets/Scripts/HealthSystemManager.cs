using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystemManager : MonoBehaviour
{
    [Header("Health on start")]
    public float Health = 100f;

    [Header("How long the dead model shows for")]
    public float ModelDeadTime = 10f;

    

    [Header("HealthText")]
    public TextMeshPro HealthText;

    [Header("DONT TOUCH")]
    public float MaxHealth = 100f;



    void Start()
    {
        MaxHealth = Health;
        HealthText.text = Health.ToString("F2");
    }

    
}
