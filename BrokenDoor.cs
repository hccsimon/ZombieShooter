using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenDoor : MonoBehaviour
{
    public AudioSource BrokenSound;
    public ParticleSystem Boom;

    public static bool UIShow = false;
    public Text DurabilityUI;
    public GameObject DurabilityUIobject;

    int DoorDurability = 120;
    
    void Update()
    {   
        DurabilityUI.text = "Door : " + DoorDurability + "/120"; 

        if(DoorDurability <=0)
        {
            DurabilityUIobject.SetActive(false);
            BrokenSound.Play();
            Boom.Play();
            Destroy(gameObject);
        }

        if(UIShow && DoorDurability > 0 )
        {
            DurabilityUIobject.SetActive(true);
        }
        else
        {
            DurabilityUIobject.SetActive(false);
        }
    }

    public void DurabilityDown(int Damage)
    {
        DoorDurability -=Damage;
    }
}
