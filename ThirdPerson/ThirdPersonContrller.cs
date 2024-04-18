using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonContrller : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedJoystick RightJoystick;
    public FixedTouchField TouchField;
    public FixedButton FireButton;
    public FixedButton ReloadButton;

    public PlayerController Control;
    public Gun GunScript;

    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.1f;//0.1f //2f
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.002f; //0.002f //0.05f

    void Update()
    {
        GunScript.isFiring = FireButton.Pressed;
        GunScript.isReloading = ReloadButton.Pressed;


        //Control.hInput =Input.GetAxis("Horizontal");
        //Control.vInput =Input.GetAxis("Vertical");
        Control.hInput =LeftJoystick.Horizontal;
        Control.vInput =LeftJoystick.Vertical;
        
        CameraAngle += TouchField.TouchDist.x * CameraAngleSpeed;
        CameraPosY = Mathf.Clamp(CameraPosY + TouchField.TouchDist.y * CameraPosSpeed, 1f , 4f);

        //CameraAngle += RightJoystick.Horizontal * CameraAngleSpeed;
        //CameraPosY = Mathf.Clamp(CameraPosY + RightJoystick.Vertical * CameraPosSpeed, 1f , 4f);

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up)* new Vector3(0, CameraPosY, 2);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
    }

}
