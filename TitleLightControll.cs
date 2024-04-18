using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLightControll : MonoBehaviour
{
    public Light Light;
    public GameObject Zombie;

    float Time_f;
    int Time_i;
    int flickerTime;

    void Update()
    {

        Time_f += Time.deltaTime;
        Time_i = (int)Time_f;

        if(Time_i == 3)
        {
            flicker();
            flickerTime++;
            Time_f = 0;
            Time_i = 0;
        }

        if(flickerTime == 4)
        {
            zombieAppear();
        }else if(flickerTime == 5)
        {
            zombieDispear();
            flickerTime =0;
        }

    }

    private void flicker()
    {
        Light.enabled = false;
        Invoke("flickerEnd",0.1f);
    }
    private void flickerEnd()
    {
        Light.enabled =true;
    }

    private void zombieAppear()
    {
        Zombie.SetActive(true);
    }
    private void zombieDispear()
    {
        Zombie.SetActive(false);
    }
}
