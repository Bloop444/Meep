using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [Header("Join the discord: https://discord.gg/WCtAftkKTy")]
    [Header("This script was made by TeeTheHuman!")]
    [Header("Set this to the name of your scene!")]
    public string SceneToLoad;
    private const string SceneLoadedKey = "SceneLoaded";

    void Start()
    {
        if (!PlayerPrefs.HasKey(SceneLoadedKey))
        {
            SceneManager.LoadScene(SceneToLoad);
            PlayerPrefs.SetInt(SceneLoadedKey, 1);
            PlayerPrefs.Save();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TutorialScript))]
public class TutorialScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TutorialScript tutorialScript = (TutorialScript)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteKey("SceneLoaded");
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs reset.");
        }
    }
}
#endif
