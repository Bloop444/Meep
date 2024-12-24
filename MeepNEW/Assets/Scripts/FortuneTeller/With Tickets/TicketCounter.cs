using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketCounter : MonoBehaviour
{
    public TextMeshPro ticketCounter;
    public int tickets;

    private void Start()
    {
        tickets = PlayerPrefs.GetInt("ticketAmount", 0);
    }

    private void Update()
    {
        ticketCounter.text = "Tickets: " + tickets.ToString();

        PlayerPrefs.SetInt("ticketAmount", tickets);
    }

    public void Buy()
    {
        if(tickets >= 5)
        {
            tickets -= 5;
        }
    }

    public void Collect()
    {
        tickets += 1;
    }
}
