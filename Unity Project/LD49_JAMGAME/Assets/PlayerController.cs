using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;
    public Camera Camera;

    public float HorizontalRotateSpeed= 1f;
    public float VerticalRotateSpeed = 1f;
    public float MoveSpeed;
    public float Gravity = 9.81f;

    float xRotation, yRotation;
    float ySpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * HorizontalRotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * VerticalRotateSpeed;


        transform.eulerAngles += new Vector3(0, mouseX, 0);
        Camera.transform.eulerAngles -= new Vector3(mouseY, 0, 0);

        float horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        float vertical = Input.GetAxis("Vertical") * MoveSpeed;
        CharacterController.Move((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime);
        
        // Gravity
        if (CharacterController.isGrounded)
        {
            ySpeed = 0;
        }
        else
        {
            ySpeed -= Gravity * Time.deltaTime;
            CharacterController.Move(new Vector3(0, ySpeed, 0));
        }
    }
}
