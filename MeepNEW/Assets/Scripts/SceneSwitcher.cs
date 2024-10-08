using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneSwitcher : MonoBehaviour
{

    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            SceneManager.LoadScene(sceneName);

            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
        }
    }
}