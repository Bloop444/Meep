using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using TMPro;

public class ColorManager : MonoBehaviour
{
    public float Red;
    public float Green;
    public float Blue;
    public TextMeshProUGUI RedDisplay;
    public TextMeshProUGUI GreenDisplay;
    public TextMeshProUGUI BlueDisplay;
    // Start is called before the first frame update
    void Start()
    {
        Red = PlayerPrefs.GetFloat("RedColor");
        Green = PlayerPrefs.GetFloat("GreenColor");
        Blue = PlayerPrefs.GetFloat("BlueColor");
        if(PlayerPrefs.HasKey("RedColor") || PlayerPrefs.HasKey("GreenColor")|| PlayerPrefs.HasKey("BlueColor"))
        {
            Color SavedColor = new Color(Red, Green, Blue);
            PhotonVRManager.SetColour(SavedColor);
        }
        Color SetColorOnStart = new Color(Red, Green, Blue);
        PhotonVRManager.SetColour(SetColorOnStart);
        if(PlayerPrefs.HasKey("RedColor") == false || PlayerPrefs.HasKey("GreenColor") == false || PlayerPrefs.HasKey("BlueColor") == false)
        {
            Red = Random.Range(0,9);
            Green = Random.Range(0,9);
            Blue = Random.Range(0,9);
            Color RandomColor = new Color(Red,Green,Blue);
            PhotonVRManager.SetColour(RandomColor);
            PlayerPrefs.SetFloat("RedColor", (int)Red);
            PlayerPrefs.SetFloat("GreenColor", (int)Green);
            PlayerPrefs.SetFloat("BlueColor", (int)Blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("RedColor", (int)Red);
        PlayerPrefs.SetFloat("GreenColor", (int)Green);
        PlayerPrefs.SetFloat("BlueColor", (int)Blue);
        RedDisplay.text = "Red: " + (int)Red;
        GreenDisplay.text = "Green: " + (int)Green;
        GreenDisplay.text = "Blue: " + (int)Blue;
        Red = (int)Red;
        Green = (int)Green;
        Blue = (int)Blue;
    }
    public void SetColor()
    {
        Color ColorToSet = new Color(Red, Green, Blue);
        PhotonVRManager.SetColour(ColorToSet);
    }
}
