using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Text BloodUI;
    public Text Money;
    public GameObject AllUI ,AK47 , DeadUI;
    public FixedJoystick joystick;
    
    
    public AudioSource ShopSound , PlayerHurtSound;

    
    [Header("Movement")]
    public float moveSpeed;
    
    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public static float PlayerHealth = 100;
    public float MoneyGet;
    

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        PlayerHealth = 100;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation =true;
        MoneyGet = 10000;
    }

    private void Update()
    {
        MyInput();
        Money.text = "$  :  " + MoneyGet;
    }
    
    private void FixedUpdate()
    {   if(PlayerHealth >0)
        {
            MovePlayer();
        }
    }

//------------------------

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //horizontalInput = joystick.Horizontal;
        //verticalInput = joystick.Vertical;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
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
    }
}
