using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.VR;

public class ColorManager : MonoBehaviour
{
    public TextMeshPro RedDisplay;
    public TextMeshPro GreenDisplay;
    public TextMeshPro BlueDisplay;
    public float RedVar;
    public float GreenVar;
    public float BlueVar;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Red") || PlayerPrefs.HasKey("Green") || PlayerPrefs.HasKey("Blue"))
        {
            Color SavedColor = new Color(PlayerPrefs.GetFloat("Red"), PlayerPrefs.GetFloat("Green"), PlayerPrefs.GetFloat("Blue"));
            PhotonVRManager.SetColour(SavedColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RedDisplay.text = "Red: " + RedVar;
        GreenDisplay.text = "Green: " + GreenVar;
        BlueDisplay.text = "Blue: " + BlueVar;
    }
    internal void SetAndSaveColor()
    {
        Color ColorToSet = new Color(RedVar, GreenVar, BlueVar);
        PlayerPrefs.SetFloat("Red", RedVar);
        PlayerPrefs.SetFloat("Green", GreenVar);
        PlayerPrefs.SetFloat("Blue", BlueVar);
        PhotonVRManager.SetColour(ColorToSet);
    }
}
