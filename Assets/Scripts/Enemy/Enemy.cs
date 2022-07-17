using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int minSpeed = 1;
    [SerializeField] private int maxSpeed = 10;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private int randomTime = 1;

    private bool faceRight;
    private float currentSpeed;
    private float currentCD;
    private int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        faceRight = true;
        CalculateSpeed();
        currentCD = randomTime;
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidbody.velocity = new Vector3(currentSpeed, m_rigidbody.velocity.y, 0);
        //CountDown();
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
}
