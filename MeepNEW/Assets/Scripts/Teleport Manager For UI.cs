using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManagerForUI : MonoBehaviour
{
    public string SceneName;
    public GameObject[] CurrentUI;
    public GameObject[] NextUI;
    public void SceneChange()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void NextPage()
    {
        foreach(GameObject obj in CurrentUI) 
        { 
            obj.SetActive(false);
        }
        foreach(GameObject obj in NextUI)
        {
            obj.SetActive(true);
        }
    }
}
