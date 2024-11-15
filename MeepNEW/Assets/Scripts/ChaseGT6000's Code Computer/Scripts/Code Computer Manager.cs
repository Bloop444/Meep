using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeComputerManager : MonoBehaviour
{
    public TextMeshProUGUI DisplayText;
    public string CodeToJoin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CodeToJoin.Length > 11)
        {
            CodeToJoin = CodeToJoin.Remove(CodeToJoin.Length - 1);
        }
        DisplayText.text = "Join Room; " + CodeToJoin;
    }
}
