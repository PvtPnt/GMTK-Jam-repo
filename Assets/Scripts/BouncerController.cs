using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncerController : MonoBehaviour
{
    public int currentJump, currentSpeed;
    public int JumpHeight, MoveSpeed;
    public float speedCap;
    float moveDirection;
    public TextMeshProUGUI Indicator;
    Rigidbody rgbd;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();

        currentJump = Random.Range(1, 6);
        currentSpeed = 6 - JumpHeight;

        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Indicator.text = "Jump Height: " + currentJump + "\nMove Speed: " + currentSpeed;

    }

    private void FixedUpdate()
    {
        Movement();

        if (rgbd.velocity.y == 0.00f)
        {
            Jump();
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rgbd.AddForce(Vector3.left * currentSpeed * 20, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rgbd.AddForce(Vector3.right * currentSpeed * 20, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rgbd.AddForce(Vector3.down * MoveSpeed * 15, ForceMode.Force);
        }

        //Cap max speed
        speedCap = currentSpeed * 10;
        if (Mathf.Abs(rgbd.velocity.x) > speedCap)
        {
            Vector3 cappedX_velocity = Vector3.ClampMagnitude(rgbd.velocity, speedCap);
            rgbd.velocity = new Vector3(cappedX_velocity.x, rgbd.velocity.y, rgbd.velocity.z);
        }
    }

    void Jump()
    {
        currentJump = JumpHeight;
        currentSpeed = MoveSpeed;

        rgbd.AddForce(Vector3.up * JumpHeight * 5f, ForceMode.Impulse);

        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
    }
}
