using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class CurrencyPickup : MonoBehaviour
{
    public int currencyAmount = 10; // Change this to the amount you want to add
    public TextMeshPro currencyText; // Reference to the currency text UI
    public string currencyName = "CC"; // Name of the currency
    public Playfablogin playfabLogin; // Reference to the Playfablogin script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("HandTag"))
        {
            // Add currency
            AddCurrency(currencyAmount);
            // Disable the pickup object
            gameObject.SetActive(false);
        }
    }

    void AddCurrency(int amount)
    {
        // Add currency to the player's account
        var request = new AddUserVirtualCurrencyRequest
        {
            Amount = amount,
            VirtualCurrency = currencyName
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnError);
    }

    void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        // Update currency display directly
        currencyText.text = "You have " + result.Balance.ToString() + " " + currencyName;
    }


    void OnError(PlayFabError error)
    {
        Debug.LogError("Error adding currency: " + error.ErrorMessage);
    }

    public void UpdateCurrencyDisplay()
    {
        // Update the currency text display
        currencyText.text = "You have " + currencyName + " " + currencyName;
    }
}
