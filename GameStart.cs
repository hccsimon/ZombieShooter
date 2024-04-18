using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public GameObject zombie;
    public AudioSource Scary;

    //Fade
    public CanvasGroup canvasGroup;
    
    float timer0 = 0;
    float timer1 = 1;
    float timer2 = 3;
    bool SceneFade = false;
    bool NotClick = true;

    int lottery;
    int JackPot;


    void Start()
    {
        zombie.SetActive(false);
        lottery = Random.Range(0,3);
        JackPot = Random.Range(0,3);
        Debug.Log(lottery);
        Debug.Log(JackPot);


        PlayerController.escapeBool = false;  //Reset static data
        PlayerController.PlayerHealth = 100; 
        PlayerController.MoneyGet = 10000;
        MachineGun.MachineGunTime = false;
        Zombie.TotalKill = 0;
        Gun.ammo = 30;
        Gun.Allammo = 60;
    }

    void Update()
    {
        timer0 += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && NotClick == true && timer0 > 2f)
        {
            ChangeScene();
            SceneFade =true;
            NotClick = false;
        }

        if(SceneFade)
        {
            timer1 += Time.deltaTime;
            canvasGroup.alpha = timer1/3;
        }

        if(SceneFade ==false)
        {
            
            timer2 -= Time.deltaTime;
            canvasGroup.alpha = timer2/1;
        }
    }


    void ChangeScene()
    {
        if(lottery == JackPot)
        {
            zombie.SetActive(true);
            Scary.Play();
        }

        PlayerController.PlayerHealth = 100; //Reset data
        PlayerController.MoneyGet = 10000;
        MachineGun.MachineGunTime = false;
        Gun.ammo = 30;
        Gun.Allammo = 60;
        
        Invoke("Loading",3f);

    }

    void Loading()
    {
        Application.LoadLevel(1);
    }
}
