using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.VR;

public class SliderScript : MonoBehaviour
{
    public Slider sliderRed;
    public Slider sliderGreen;
    public Slider sliderBlue;

    public TMP_Text redPercentageText;
    public TMP_Text greenPercentageText;
    public TMP_Text bluePercentageText;

    public Image redFillImage;
    public Image greenFillImage;
    public Image blueFillImage;

    void Update()
    {
        Color myColour = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);
        PhotonVRManager.SetColour(myColour);

        redPercentageText.text = (sliderRed.value * 10).ToString("F0");
        greenPercentageText.text = (sliderGreen.value * 10).ToString("F0");
        bluePercentageText.text = (sliderBlue.value * 10).ToString("F0");

        redFillImage.color = new Color(sliderRed.value, 0, 0);
        greenFillImage.color = new Color(0, sliderGreen.value, 0);
        blueFillImage.color = new Color(0, 0, sliderBlue.value);

        sliderRed.fillRect.GetComponent<Image>().color = new Color(sliderRed.value, 0, 0);
        sliderGreen.fillRect.GetComponent<Image>().color = new Color(0, sliderGreen.value, 0);
        sliderBlue.fillRect.GetComponent<Image>().color = new Color(0, 0, sliderBlue.value);
    }
}
