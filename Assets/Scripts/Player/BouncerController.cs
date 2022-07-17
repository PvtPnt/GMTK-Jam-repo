using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncerController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator JumpDice, SpeedDice;
    [SerializeField] int JumpHeight, MoveSpeed;
    Rigidbody rgbd;
    [SerializeField] int speedMultiplier = 20;
    [SerializeField] int heightMultiplier = 5;
    [SerializeField] float fallMultiplier = 2.5f;
    bool grounded;
    float speedCap;

    //Status timer
    float isSideAttack, isReverseControl, isGodMode, isAds, isDarts;

    //Status Max Timer
    [Header("Max Status Duration")]
    [SerializeField] float invisibleCd = 5;
    [SerializeField] float dartCd = 6, adsCd = 10, godModeCd = 5, confusedCd = 5;

    //Materials
    [Header("Materials")]
    [SerializeField] Material m_invisible;
    [SerializeField] Material m_berserk, m_confuse;

    Vector3 startPos;
    [SerializeField] RandomStatus randomStatus;
    [SerializeField] GameObject SideAttackCollider;

    [SerializeField] GameObject PauseMenu;

    [Header("UI")]
    [SerializeField] GameObject UI_invisible;
    [SerializeField] GameObject UI_berserk;
    [SerializeField] GameObject UI_confuse;
    [SerializeField] GameObject UI_ads;
    [SerializeField] GameObject UI_darts;
    private Material m_ori;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody>();
        JumpHeight = 3;
        MoveSpeed = 3;
        startPos = transform.position;
        m_ori = GetComponent<Renderer>().material;
        isReverseControl = 0;
        isSideAttack = 0;
        isGodMode = 0;
        isAds = 0;
        isDarts = 0;

        //Debug statuses
        //SetEverythingOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReverseControl > 0)
        {
            isReverseControl -= Time.deltaTime;
            if (isReverseControl <= 0)
            {
                GetComponent<Renderer>().material = m_ori;
                UI_confuse.SetActive(false);
            }
        }

        if (isSideAttack > 0)
        {
            isSideAttack -= Time.deltaTime;
            if (isSideAttack <= 0)
            {
                GetComponent<Renderer>().material = m_ori;
                UI_berserk.SetActive(false);
            }
        }

        if (isGodMode > 0)
        {
            isGodMode -= Time.deltaTime;
            if (isGodMode <= 0)
            {
                GetComponent<Renderer>().material = m_ori;
                UI_invisible.SetActive(false);
            }
        }

        if (isAds > 0)
        {
            isAds -= Time.deltaTime;
            if (isAds <= 0)
            {
                GetComponent<Renderer>().material = m_ori;
                UI_ads.SetActive(false);
            }
        }

        if (isDarts > 0)
        {
            isDarts -= Time.deltaTime;
            if (isDarts <= 0)
            {
                UI_darts.SetActive(false);
            }
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
        if (isReverseControl > 0)
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
        if (rgbd.velocity.y < 0)
        {
            playerAnimator.SetTrigger("Fall");
            rgbd.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        JumpHeight = Random.Range(1, 6);
        MoveSpeed = 6 - JumpHeight;
        UpdateDice();
        playerAnimator.SetTrigger("Jump");
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
        else if (colTag == "Spike" && isGodMode <= 0)
        {
            Respawn();
        }
        else if (colTag == "Bullet" && isGodMode <= 0)
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
        else if (other.gameObject.tag == "Dart")
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
        if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
        {
            // side collision
            if (isSideAttack > 0)
            {
                collision.gameObject.SetActive(false);
                RandomStatus.SharedInstance.GetRandomStatus();
            }
            else if (isGodMode <= 0)
            {
                Respawn();
            }
        }
        else if (Mathf.Abs(dist.x) < Mathf.Abs(dist.y))
        {
            // top collision
            collision.gameObject.SetActive(false);
            RandomStatus.SharedInstance.GetRandomStatus();
        }

    }

    public void SetSideAttack()
    {
        isSideAttack = invisibleCd;
        GetComponent<Renderer>().material = m_berserk;
        UI_berserk.SetActive(true);
    }

    public void SetReverseControl()
    {
        isReverseControl = confusedCd;
        GetComponent<Renderer>().material = m_confuse;
        UI_confuse.SetActive(true);
    }

    public void SetGodMode()
    {
        isGodMode = godModeCd;
        GetComponent<Renderer>().material = m_invisible;
        UI_invisible.SetActive(true);
    }

    public void SetAds()
    {
        isAds = adsCd;
        UI_ads.SetActive(true);
    }

    public void SetDarts()
    {
        isDarts = dartCd;
        UI_darts.SetActive(true);
    }

    void SetEverythingOff()
    {
        SetSideAttack();
        SetReverseControl();
        SetGodMode();
        SetDarts();
        SetAds();
    }
}