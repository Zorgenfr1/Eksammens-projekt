using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("1") && Input.GetButton("�"))
        {
            acceleration = 5f;
            speed = 10f;
            jumpForce = 5f;
            gravity = 6f;
            mouseSensitivity = 10f;
        }
        if (Input.GetButtonDown("2") && Input.GetButton("�"))
        {
            acceleration = 10f;
            speed = 10f;
            jumpForce = 5f;
            gravity = 6f;
            mouseSensitivity = 10f;
        }
        if (Input.GetButtonDown("3") && Input.GetButton("�"))
        {
            acceleration = 5f;
            speed = 20f;
            jumpForce = 5f;
            gravity = 6f;
            mouseSensitivity = 10f;
        }
        if (Input.GetButtonDown("4") && Input.GetButton("�"))
        {
            acceleration = 5f;
            speed = 10f;
            jumpForce = 10f;
            gravity = 6f;
            mouseSensitivity = 10f;
        }
        if (Input.GetButtonDown("5") && Input.GetButton("�"))
        {
            acceleration = 5f;
            speed = 10f;
            jumpForce = 5f;
            gravity = 12f;
            mouseSensitivity = 10f;
        }
        if (Input.GetButtonDown("6") && Input.GetButton("�"))
        {
            acceleration = 5f;
            speed = 10f;
            jumpForce = 5f;
            gravity = 6f;
            mouseSensitivity = 20f;
        }
    }
}
