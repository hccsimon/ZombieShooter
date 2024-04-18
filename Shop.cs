using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Gun GunScript;
    public PlayerMovement PlayerScript;
    public PlayerController PlayerController;
    public FixedButton Ammo_Button;
    public FixedButton Health_Button;


    void Update()
    {
        /*if(Gun.WeaponShoping && Ammo_Button.Pressed && PlayerController.MoneyGet >=2000 && PlayerController.PlayerHealth>0)
        {
            GunScript.BuyAmmo();
        }

        if(Gun.HealthShoping && Health_Button.Pressed &&PlayerController.MoneyGet >=5000 && PlayerController.PlayerHealth>0)
        {
            GunScript.BuyHealth();
        }*/

        /*if(Gun.WeaponShoping && Input.GetKeyDown(KeyCode.B) && PlayerScript.MoneyGet >=2000 && PlayerMovement.PlayerHealth>0)
        {
            GunScript.BuyAmmo();
        }

        if(Gun.HealthShoping && Input.GetKeyDown(KeyCode.B) &&PlayerScript.MoneyGet >=5000 && PlayerMovement.PlayerHealth>0)
        {
            GunScript.BuyHealth();
        }*/
    }

    public void Shopping()
    {
        if(Gun.WeaponShoping && PlayerController.MoneyGet >=2000 && PlayerController.PlayerHealth>0)
        {
            GunScript.BuyAmmo();
        }

        if(Gun.HealthShoping && PlayerController.MoneyGet >=5000 && PlayerController.PlayerHealth>0)
        {
            GunScript.BuyHealth();
        }
    }
}
