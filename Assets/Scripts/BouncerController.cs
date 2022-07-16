using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncerController : MonoBehaviour
{
    public int JumpHeight, MoveSpeed;
    public TextMeshProUGUI Indicator;
    Rigidbody rgbd;
    public int speedMultiplier = 20;
    public int heightMultiplier = 5;
    float speedCap;
    bool grounded;
    bool isSideAttack;
    [SerializeField] RandomStatus randomStatus;
    [SerializeField] GameObject SideAttackCollider;

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

        //if (rgbd.velocity.y == 0.00f)
        //{
        //    Jump();
        //}

    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rgbd.AddForce(Vector3.left * MoveSpeed * speedMultiplier, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rgbd.AddForce(Vector3.right * MoveSpeed * speedMultiplier, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rgbd.AddForce(Vector3.down * MoveSpeed * speedMultiplier, ForceMode.Force);
        }

        //Cap max speed
        speedCap = MoveSpeed * 10;
        if (Mathf.Abs(rgbd.velocity.x) > speedCap)
        {
            Vector3 cappedX_velocity = Vector3.ClampMagnitude(rgbd.velocity, speedCap);
            rgbd.velocity = new Vector3(cappedX_velocity.x, rgbd.velocity.y, rgbd.velocity.z);
        }
    }

    void Jump()
    {
        rgbd.AddForce(Vector3.up * JumpHeight * 5f, ForceMode.Impulse);
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Jump();
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}