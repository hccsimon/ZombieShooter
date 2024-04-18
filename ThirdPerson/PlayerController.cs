using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public CharacterController player;
    public FixedJoystick joystick;

    public Animator PlayerAnim;

    [HideInInspector]
    public Vector3 dir;
    public float hInput , vInput;

    float groundYOffset;
    LayerMask groundMask;
    Vector3 spherePos;
    float gravity =-9.81f;
    Vector3 velocity;
    Vector3 camDir;


    [Header("Setting")]
    public Text BloodUI;
    public Text Money;
    public GameObject AllUI ,AK47 , DeadUI , WinUI;
    public AudioSource ShopSound , PlayerHurtSound;
    public static float PlayerHealth = 100;
    public static float MoneyGet = 10000;
    public static bool escapeBool = false;

    private void Start()
    {
        escapeBool = false;
        AllUI.SetActive(true);
        AK47.SetActive(true);
        WinUI.SetActive(false);
        DeadUI.SetActive(false);

        BloodUI.text = "" + PlayerHealth;
    }    

    void Update()
    {
        Money.text = "$  :  " + MoneyGet;
        camDir = Camera.main.transform.forward;
        camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
        if(PlayerHealth >0 && !escapeBool)
        {
            GetDirectionAndMove();
            Gravity();
        }
        
    }

    void GetDirectionAndMove()
    {
        //hInput = joystick.Horizontal;
        //vInput = joystick.Vertical;
        if(Mathf.Abs(vInput) > 0 || Mathf.Abs(hInput) >0)
        {
            PlayerAnim.SetBool("Walk",true);
        }else
        {
            PlayerAnim.SetBool("Walk",false);
        }
        dir = transform.forward * vInput + transform.right * hInput;
        player.Move(dir * moveSpeed * Time.deltaTime);
        player.transform.forward = camDir;
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x , transform.position.y - groundYOffset ,transform.position.z );
        if(Physics.CheckSphere(spherePos , player.radius - 0.05f , groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if(!isGrounded()) velocity.y += gravity * Time.deltaTime;
        else if(velocity.y <0 ) velocity.y = -2;

        player.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos,player.radius - 0.05f);
    }

    public void PlayerHurt(float amount)
    {
        if(PlayerHealth > 0)
        {
        PlayerHurtSound.Play();
        PlayerHealth -= amount;
        BloodUI.text = "" + PlayerHealth;
        BloodUI.color = Color.white;
        }

        if(PlayerHealth <= 30)
        {
            BloodUI.color = Color.red;
            BloodUI.text = "" + PlayerHealth;
        }

        if(PlayerHealth <=0)
        {
            PlayerDead();
        }
    }

    public void PlusPoint(float amount)
    {   
        MoneyGet += amount;
        Money.text = "$  :  " + MoneyGet;
    }

    public void Buy(int product)
    {
        ShopSound.Play();
        if(product ==1)//weapon shop
        {
            MoneyGet -= 2000;
        }else if(product ==2 && PlayerHealth <100)
        {
            MoneyGet -= 5000;
            PlayerHealth = 100;
            BloodUI.text = "" + PlayerHealth;
            BloodUI.color = Color.white;
        }
    }

    void PlayerDead()
    {
        AllUI.SetActive(false);
        AK47.SetActive(false);
        DeadUI.SetActive(true);
        PlayerAnim.SetBool("Walk",false);
        PlayerAnim.SetBool("Fire",false);
        PlayerAnim.SetBool("Reload",false);
        PlayerAnim.SetTrigger("Dead");
    }

    public void escapeWin() //escape
    {
            escapeBool = true;
            AllUI.SetActive(false);
            AK47.SetActive(false);
            WinUI.SetActive(true);
            PlayerAnim.SetBool("Walk",false);
            PlayerAnim.SetBool("Fire",false);
            PlayerAnim.SetBool("Reload",false);
    }
}