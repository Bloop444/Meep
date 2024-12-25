using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;

public class CookieClickerUpdate : MonoBehaviour
{
    public string FingerTag;
    public int Cookies;
    public TextMeshPro CookieText;

    private const string CookiesKey = "Cookies";

    private void Start()
    {
        Cookies = PlayerPrefs.GetInt(CookiesKey, 0);
        UpdateCookieText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(FingerTag))
        {
            Cookies++;
            UpdateCookieText();
            SaveCookies();
        }
    }

    private void UpdateCookieText()
    {
        if (CookieText != null)
        {
            CookieText.text = Cookies.ToString();
        }
    }

    private void SaveCookies()
    {
        PlayerPrefs.SetInt(CookiesKey, Cookies);
        PlayerPrefs.Save();
    }

    public void TestPressButton()
    {
        Cookies++;
        UpdateCookieText();
        SaveCookies();
    }

    private void OnApplicationQuit()
    {
        SaveCookies();
    }
}

[CustomEditor(typeof(CookieClickerUpdate))]
public class CookieClickerUpdateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CookieClickerUpdate script = (CookieClickerUpdate)target;

        if (GUILayout.Button("Test Press"))
        {
            script.TestPressButton();
        }
    }
}
