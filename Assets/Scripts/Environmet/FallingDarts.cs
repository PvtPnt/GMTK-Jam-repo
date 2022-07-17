using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDarts : MonoBehaviour
{
    [SerializeField] private int fallingTimer;
    [SerializeField] private int fallingCD;
    [SerializeField] private float offsetRange;
    [SerializeField] private GameObject darts;
    [SerializeField] private float offsetY = 30;
    [SerializeField] private float fallingForce = 30;

    private GameObject player;
    private bool enableFallingDarts;
    private float currentTimer;     // how long falling lasts
    private float currentCD;        // time between each darts
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enableFallingDarts = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFalling();
    }
    public void EnableFallingDarts()
    {
        enableFallingDarts = true;
        currentCD = fallingCD;
        currentTimer = fallingTimer;
    }

    private void UpdateFalling()
    {
        if (!enableFallingDarts) return;

        currentCD -= Time.deltaTime;
        currentTimer -= Time.deltaTime;

        if(currentCD <= 0)
        {
            currentCD = fallingCD;
            // instantiate falling darts
            Vector3 startPos = player.transform.position;
            startPos.y += offsetY;
            startPos.x += Random.Range(-offsetRange, offsetRange);

            Vector3 force = Vector3.down * fallingForce;

            GameObject new_darts = GameObject.Instantiate(darts);
            new_darts.transform.position = startPos;
            new_darts.GetComponent<Rigidbody>().AddForce(force);
        }

        if (currentTimer <= 0) enableFallingDarts = false;
    }
}
