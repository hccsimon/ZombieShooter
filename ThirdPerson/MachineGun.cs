using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public static bool MachineGunTime = false;
    public GameObject Player;
    public GameObject Maincam;
    public GameObject TopGun;
    public GameObject GunPart;
    public GameObject MGHitBox;
    public AudioListener AudioListener;

    public GameObject EnterButton;
    public GameObject ExitButton;

    //Movement
    public FixedTouchField TouchField;
    public FixedButton FireButton;
    protected float GunAngle;
    protected float GunAngleSpeed = 0.15f;
    protected float GunAngleY;
    protected float GunAngleYSpeed = 0.2f; 

    //Shooting
    public Camera RayCamera;
    public LayerMask IgnoreMe;
    public float timeBetweenBullets = 0.15f;
    private float nextTimeToFire = 0f;
    float effectsDisplayTime = 0.2f;

    public AudioSource GunSound;
    public ParticleSystem muzzleFlash;
    public Light gunLight;

    public float damage = 20f;
    public float range = 1000f;
    public float fireRate = 15f;

    float timer;

    [HideInInspector]
    public bool isFiring = false;
    

    void Update()
    {
        timer += Time.deltaTime;

        if(MachineGunTime == true)
        {
            UseMachineGun();
        }
    }

    public void UseMachineGun()
    {
        Maincam.SetActive(false);
        Player.SetActive(false);
        ExitButton.SetActive(true);

        GunAngle = Mathf.Clamp(GunAngle + TouchField.TouchDist.x * GunAngleSpeed , 150f,290f); //Move
        GunAngleY = Mathf.Clamp(GunAngleY + TouchField.TouchDist.y * GunAngleYSpeed , -5f,10f);
        TopGun.transform.rotation =  Quaternion.Euler(GunAngleY,GunAngle,0);
        GunPart.transform.rotation = TopGun.transform.rotation;

        

        isFiring = FireButton.Pressed;

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            gunLight.enabled = false;
        }

        if(isFiring && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

    }

    void Shoot()
    {
        muzzleFlash.Play();
        GunSound.Play();
        gunLight.enabled = true;

        RaycastHit hit;
        if(Physics.Raycast(RayCamera.transform.position, RayCamera.transform.forward, out hit, range, ~IgnoreMe))
        {
            Zombie Zombie =hit.transform.GetComponent<Zombie>();
            if(Zombie !=null)
            {
                Zombie.TakeDamage(damage);
            }
        }
    }



    //Trigger

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag =="Player")
        {
            EnterButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag =="Player")
        {
            EnterButton.SetActive(false);
        }
    }



//Button Function
    public void EnterFunction()
    {
        MachineGunTime = true;
        MGHitBox.SetActive(true);
        AudioListener.enabled = true;
    }
    public void ExitFunction()
    {
        AudioListener.enabled = false;
        MachineGunTime = false;
        Maincam.SetActive(true);
        Player.SetActive(true);
        ExitButton.SetActive(false);
        MGHitBox.SetActive(false);
    }
}
