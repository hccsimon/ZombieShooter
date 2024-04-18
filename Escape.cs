using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public PlayerController PlayerScript;


    public void OnTriggerEnter(Collider escape) //escape
    {
        if(escape.gameObject.tag =="Escape")
        {
            print("escae");
            PlayerScript.escapeWin();
        }
    }

    public void NextStage()
    {
        Application.LoadLevel(2);
    }
}
