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
    Vector3 startPos;
    [SerializeField] RandomStatus randomStatus;
    [SerializeField] GameObject SideAttackCollider;
    [SerializeField] GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        JumpHeight = 3;
        MoveSpeed = 3;
        UpdateDebug();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && PauseMenu.activeSelf == false)
        {
            PauseMenu.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Movement();
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
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
        UpdateDebug();
        rgbd.AddForce(Vector3.up * JumpHeight * heightMultiplier, ForceMode.Impulse);
    }

    void UpdateDebug()
    {
        Indicator.text = "Jump: " + JumpHeight + "\nSpeed: " + MoveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.gameObject.tag;
        if(colTag == "Ground")
        {
            //Reset y velocity so that the jump impulse is accurate
            rgbd.velocity = new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z);
            Jump();
        }
        else if(colTag == "Enemy" || colTag == "Spike")
        {
            gameObject.transform.position = startPos;
            rgbd.velocity = Vector3.zero;
        }
    }
}