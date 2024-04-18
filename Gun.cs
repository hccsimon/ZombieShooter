using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public Animator GunAnim;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public LayerMask IgnoreMe;
    public ParticleSystem muzzleFlash;
    public Text AmmoAmount;
    public AudioSource GunSound;
    public AudioSource GunReload;
    public AudioSource NoAmmo;
    public Animator GunAnim;
    public PlayerMovement PlayerScript;
    public PlayerController PlayerController;
    public Recoil RecoilScript;
    public Animator PlayerAnim;

    public GameObject AmmoButton;
    public GameObject HealthButton;

    public Light gunLight;
    public float timeBetweenBullets = 0.15f;
    float effectsDisplayTime = 0.2f;
    float timer;

    public static bool WeaponShoping = false;
    public static bool HealthShoping = false;

    public static int ammo = 30;
    public static int Allammo = 60;
    bool StartReload=false;
    [HideInInspector]
    public bool isFiring = false;
    [HideInInspector]
    public bool isReloading =false;
    

    private float nextTimeToFire = 0f;

    void Start()
    {
        gunLight.enabled = false;
        AmmoAmount.text =ammo + "/" +Allammo;
    }

    void Update()
    {

        timer += Time.deltaTime;

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }

        if(/*Input.GetButton("Fire1") || */isFiring && ammo >0 && !StartReload)
        {
            GunAnim.SetBool("Shooting",true);
        }else
        {
            GunAnim.SetBool("Shooting",false);
        }

        if(/*Input.GetButton("Fire1") || */isFiring && Time.time >= nextTimeToFire  && !StartReload)
        {
            if(ammo>0)
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
            }else if(ammo ==0 && Allammo ==0)
            {
                NoAmmo.Play();
            }
        }else
        {
            PlayerAnim.SetBool("Fire",false);
        }

        if(/*Input.GetKeyDown(KeyCode.R)*/isReloading && Allammo !=0 && ammo < 30 && StartReload==false)
        {
            Reload();
        }

        RaycastHit Facing;//Detect watching shop or not  
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Facing, 3 , ~IgnoreMe))
        {
            if(Facing.transform.tag == "WeaponShop")
            {
                WeaponShoping = true;
                AmmoButton.SetActive(true);
            }else
            {
                WeaponShoping = false;
                AmmoButton.SetActive(false);
            }
            
            if(Facing.transform.tag == "HealthShop")
            {
                HealthShoping = true;
                HealthButton.SetActive(true);
            }else
            {
                HealthShoping = false;
                HealthButton.SetActive(false);
            }

        }

        RaycastHit DurabilityDoor;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out DurabilityDoor, range , ~IgnoreMe))
        {
            if(DurabilityDoor.transform.tag == "Finish")
            {
                BrokenDoor.UIShow = true;
            }else
            {
                BrokenDoor.UIShow = false;
            }
        }
            

    }

    void Shoot()
    {
        muzzleFlash.Play();
        GunSound.Play();
        PlayerAnim.SetBool("Fire",true);
        ammo -= 1;
        AmmoAmount.text =ammo + "/" +Allammo;
        RecoilScript.RecoilFire();
        gunLight.enabled = true;

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range , ~IgnoreMe))
        {
            //Debug.Log(hit.transform.name);
            Zombie Zombie =hit.transform.GetComponent<Zombie>();
            if(Zombie !=null)
            {
                Zombie.TakeDamage(damage);
            }

            BrokenDoor BrokenDoor =hit.transform.GetComponent<BrokenDoor>();
            if(BrokenDoor != null)
            {
                print("Hit Door");
                BrokenDoor.DurabilityDown(1);
            }
        }
    }

    public void Reload()
    {
        StartReload =true;
        PlayerAnim.SetBool("Reload",true);
        GunAnim.SetBool("Reload",true);
        GunReload.Play();
        Invoke("ReloadEnd",3f);
    }
    public void ReloadEnd()
    {
        GunAnim.SetBool("Reload",false);
        PlayerAnim.SetBool("Reload",false);
        if(Allammo >=30)
        {
            if(ammo == 0)
            {
                AmmoAmount.text = "30/" + (Allammo-30);
                ammo = 30;
                Allammo = Allammo -30;
            }
            else
            {
                AmmoAmount.text = "30/" + Mathf.Abs(30-(Allammo + ammo));
                Allammo = Mathf.Abs(30-(Allammo + ammo));
                ammo = 30;
            }
            
        }
        else if(Allammo <30)
        {
            if(Allammo+ammo <=30)
            {
                AmmoAmount.text = ammo + Allammo + "/0";
                ammo = Allammo + ammo; 
                Allammo = 0;
            }else if(Allammo+ammo >30)
            {
                AmmoAmount.text = "30/" + Mathf.Abs(30-(Allammo + ammo));
                Allammo = Mathf.Abs(30-(Allammo + ammo));
                ammo = 30;
            }
        }
        StartReload =false;
    }

    public void DisableEffects ()
    {
        gunLight.enabled = false;
    }

    public void BuyAmmo()
    {
        Allammo += 30 ;
        PlayerController.Buy(1);
        AmmoAmount.text = ammo + "/" + Allammo;
    }
    public void BuyHealth()
    {
        PlayerController.Buy(2);
    }

    
}
