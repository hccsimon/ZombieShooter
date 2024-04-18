using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public GameObject Calling;
    public GameObject ZombieSpawner;
    public GameObject TextObject;
    public GameObject WinBoard;
    public Text CountDown2;
    bool CallingFinished = false;

    public PlayerController PlayerScript;

    public AudioSource LastBGM;
    public AudioSource RadioSound;
    public AudioSource Helicopter;
    
    float Timer_f;
    int Timer_i;
    int currentTime = 120;

    void Start()
    {
        PlayerController.escapeBool = false;
    }

    void Update()
    {
        if(CallingFinished == true && currentTime >0)  //start count down
        {
            Timer_f += Time.deltaTime;
            Timer_i = (int)Timer_f;
            currentTime = 120 - Timer_i;
            CountDown2.text = currentTime + "Second";
        }

        if(currentTime == 10)
        {
            Helicopter.Play();
        }

        if(currentTime == 0)
        {
            WinBoard.SetActive(true);
            PlayerController.escapeBool = true;
        }
    }



    public void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag == "Player" && !CallingFinished)
        {
            Calling.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider Player)
    {
        if(Player.gameObject.tag == "Player" && !CallingFinished)
        {
            Calling.SetActive(false);
        }
    }

    public void CallingButton()
    {
        TextObject.SetActive(true);
        CallingFinished = true;
        Calling.SetActive(false);
        ZombieSpawner.SetActive(true);
        LastBGM.Play();
        RadioSound.Play();

    }
}
