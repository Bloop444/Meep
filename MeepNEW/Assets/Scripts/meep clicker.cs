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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(FingerTag))
        {
            Cookies++;
            UpdateCookieText();
        }
    }

    private void UpdateCookieText()
    {
        if (CookieText != null)
        {
            CookieText.text = Cookies.ToString();
        }
    }

    public void TestPressButton()
    {
        Cookies++;
        UpdateCookieText();
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
