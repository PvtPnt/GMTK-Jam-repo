using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator enemyAnimator;
    [SerializeField] private int minSpeed = 1;
    [SerializeField] private int maxSpeed = 10;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private int randomTime = 1;
    [SerializeField] private int shootingForce = 40;

    [SerializeField] private GameObject bullet;

    private GameObject player;

    private bool faceRight;
    private float currentSpeed;
    private float currentCD;
    private int moveSpeed;

    // shooting
    [SerializeField] private bool isShooterEnemy;
    [SerializeField] private float shootCD = 2;
    [Tooltip("set how long will shooting last")]
    [SerializeField] private float shootTimer = 5;
    private bool enableShooting;
    private float currenShootCD;
    private float currenShootTimer;

    private Vector3 startPos;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        faceRight = true;
        CalculateSpeed();
        currentCD = randomTime;
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        enableShooting = isShooterEnemy;
        currenShootCD = shootCD;
        currenShootTimer = shootTimer;

        if(isShooterEnemy)
        {
            enemyAnimator.SetBool("Range", true);
        }
        else
        {
            enemyAnimator.SetBool("Range", false);
        }
    }
    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShooterEnemy)
        {
            m_rigidbody.velocity = new Vector3(currentSpeed, m_rigidbody.velocity.y, 0);
            CountDown();
        }
        UpdateShooting();
    }

    private void CalculateSpeed()
    {
        currentSpeed = moveSpeed * speedMultiplier * (faceRight ? 1 : -1);
    }

    public void SetNewSpeed(int s)
    {
        moveSpeed = s;
        CalculateSpeed();
    }

    public void Turn()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        CalculateSpeed();
    }
    private void CountDown()
    {
        currentCD -= Time.deltaTime;
        if(currentCD <= 0)
        {
            currentCD = randomTime;
            moveSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }

    public void EnableShooting()
    {
        enableShooting = true;
        currenShootCD = shootCD;
        currenShootTimer = shootTimer;
    }

    private void UpdateShooting()
    {
        if (!enableShooting && !isShooterEnemy) return;


        currenShootTimer -= Time.deltaTime;
        currenShootCD -= Time.deltaTime;

        if (currenShootCD <= 0)
        {
            currenShootCD = shootCD;
            enemyAnimator.SetTrigger("Shoot");
            // fire projectile
            Vector3 dir = (player.transform.position - transform.position).normalized;
            Vector3 startPos = transform.position + (dir * (transform.localScale.x + bullet.transform.localScale.y));
            GameObject new_bullet = Instantiate(bullet, startPos,new Quaternion());

            Vector3 current_dir = transform.up;

            new_bullet.transform.localRotation *= Quaternion.FromToRotation(current_dir, dir);

            new_bullet.GetComponent<Rigidbody>().velocity = dir * shootingForce;
        }

        // stop shooting
        if (currenShootTimer <= 0) enableShooting = false;
    }

    public void Respawn()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
        m_rigidbody.velocity = Vector3.zero;
    }
}
