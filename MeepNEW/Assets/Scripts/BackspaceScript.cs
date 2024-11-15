using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackspaceScript : MonoBehaviour
{
    public NameScript NameScript;
    public void StartBackspace()
    {
        NameScript.NameVar = NameScript.NameVar.Remove(NameScript.NameVar.Length - 1);
    }
}
