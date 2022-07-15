using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncerController : MonoBehaviour
{
    public int JumpHeight, MoveSpeed;
    public TextMeshProUGUI Indicator;
    Rigidbody rgbd;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Indicator.text = "Jump Height: " + JumpHeight + "\nMoveSpeed: " + MoveSpeed;
        Movement();
        //Jump();

        if (rgbd.velocity.y == 0.00f)
        {
            Jump();
        }

    }

    void Movement()
    {
        float MoveForce = Mathf.Clamp(MoveSpeed, 1f, 3f);
        if (Input.GetKey(KeyCode.A))
        {
            rgbd.AddForce(Vector3.left * MoveForce , ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rgbd.AddForce(Vector3.right * MoveForce, ForceMode.VelocityChange);
        }
    }

    void Jump()
    {
        float JumpForce = Mathf.Clamp(JumpHeight, 1f, 6f);
        rgbd.AddForce(Vector3.up * JumpForce * 5f, ForceMode.Impulse);
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
    }
}
