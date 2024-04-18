using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } 

    public void Update()
    {
        /*if(Touchscreen.current.touches.Count >0 && Touchscreen.current.touches[0].isInProgress)
        {
            mouseX = Touchscreen.current.touches[0].delta.ReadValue().x;
            mouseY = Touchscreen.current.touches[0].delta.ReadValue().y;
        }*/

        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation , -90f , 90f);
        
        transform.rotation = Quaternion.Euler( xRotation , yRotation , 0);
        orientation.rotation = Quaternion.Euler( 0 , yRotation , 0 );

    }

}
