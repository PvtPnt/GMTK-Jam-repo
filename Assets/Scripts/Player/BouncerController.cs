using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncerController : MonoBehaviour
{
    [SerializeField] Animator JumpDice, SpeedDice;
    [SerializeField] int JumpHeight, MoveSpeed;
    public TextMeshProUGUI Indicator;
    Rigidbody rgbd;
    [SerializeField] int speedMultiplier = 20;
    [SerializeField] int heightMultiplier = 5;
    [SerializeField] float fallMultiplier = 2.5f;
    float speedCap;
    bool grounded;
    float isSideAttack;
    float isReverseControl;
    Vector3 startPos;
    [SerializeField] RandomStatus randomStatus;
    [SerializeField] GameObject SideAttackCollider;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Material m_invisible;
    [SerializeField] Material m_confuse;
    private Material m_ori;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        JumpHeight = 3;
        MoveSpeed = 3;
        startPos = transform.position;
        m_ori = GetComponent<Renderer>().material;
        isReverseControl = 0;
        isSideAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReverseControl > 0)
        {
            isReverseControl -= Time.deltaTime;
            if(isReverseControl <= 0)
                GetComponent<Renderer>().material = m_ori;
        }
        if (isSideAttack > 0)
        {
            isSideAttack -= Time.deltaTime;
            if (isSideAttack <= 0)
                GetComponent<Renderer>().material = m_ori;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (PauseMenu.activeSelf == false)
            {
                PauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
        FallUpdate();
    }

    void Movement()
    {
        if(isReverseControl > 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rgbd.AddForce(Vector3.left * MoveSpeed * speedMultiplier, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rgbd.AddForce(Vector3.right * MoveSpeed * speedMultiplier, ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rgbd.AddForce(Vector3.down * MoveSpeed * speedMultiplier, ForceMode.Force);
            }
        }
        else
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
        }


        //Cap max speed
        speedCap = MoveSpeed * 10;
        if (Mathf.Abs(rgbd.velocity.x) > speedCap)
        {
            Vector3 cappedX_velocity = Vector3.ClampMagnitude(rgbd.velocity, speedCap);
            rgbd.velocity = new Vector3(cappedX_velocity.x, rgbd.velocity.y, rgbd.velocity.z);
        }
    }

    void FallUpdate()
    {
        if(rgbd.velocity.y < 0)
        {
            rgbd.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
        UpdateDice();
        rgbd.AddForce(Vector3.up * JumpHeight * heightMultiplier, ForceMode.Impulse);
    }

    void UpdateDice()
    {
        JumpDice.GetComponent<DiceRoll>().TargetFace = JumpHeight;
        JumpDice.SetTrigger("Roll");
        JumpDice.SetInteger("DiceFace", JumpHeight);

        SpeedDice.GetComponent<DiceRoll>().TargetFace = MoveSpeed;
        SpeedDice.SetTrigger("Roll");
        SpeedDice.SetInteger("DiceFace", MoveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.gameObject.tag;
        if (colTag == "Ground")
        {
            //Reset y velocity so that the jump impulse is accurate
            rgbd.velocity = new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z);
            Jump();
        }
        else if (colTag == "Enemy")
        {
            resolveEnemyCollision(collision);
        }
        else if (colTag == "Spike")
        {
            Respawn();
        }
        else if(colTag == "Bullet")
        {
            Destroy(collision.gameObject);
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            startPos = transform.position;
        }
        else if(other.gameObject.tag == "Dart")
        {
            Destroy(other.gameObject);
            Respawn();
        }
    }

    private void Respawn()
    {
        gameObject.transform.position = startPos;
        rgbd.velocity = Vector3.zero;
        EnemyCenter.Instance.RespawnEnemies();
    }

    private void resolveEnemyCollision(Collision collision)
    {
        Vector3 dist = (transform.position - collision.transform.position).normalized;
        if(Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            // side collision
            if (isSideAttack > 0)
            {
                collision.gameObject.SetActive(false);
                RandomStatus.SharedInstance.GetRandomStatus();
            }
            else
            {
                Respawn();
            }
        }
        else if(Mathf.Abs(dist.x) < Mathf.Abs(dist.y))
        {
            // top collision
            collision.gameObject.SetActive(false);
            RandomStatus.SharedInstance.GetRandomStatus();
        }

    }

    public void SetSideAttack()
    {
        isSideAttack = 10;
        GetComponent<Renderer>().material = m_invisible;
    }

    public void SetReverseControl()
    {
        isReverseControl = 10;
        GetComponent<Renderer>().material = m_confuse;
    }
}