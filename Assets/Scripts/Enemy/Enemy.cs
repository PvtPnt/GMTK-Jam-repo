using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 1;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private Rigidbody m_rigidbody;

    private bool faceRight;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        faceRight = true;
        CalculateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidbody.velocity = new Vector3(currentSpeed, m_rigidbody.velocity.y, 0);
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
}
