using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomStatus : MonoBehaviour
{
    public static RandomStatus SharedInstance;
    string currStatus;
    [SerializeField] BouncerController player;
    [SerializeField] FallingDarts dartsScript;
    [SerializeField] GameObject AdPanel;
    [SerializeField] float AdTime = 10;
    float adCount = 0;
    // Start is called before the first frame update

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (adCount > 0)
        {
            adCount -= Time.deltaTime;
            if (adCount <= 0)
                AdPanel.gameObject.SetActive(false);
        }
    }

    public void GetRandomStatus()
    {
        int chance = Random.Range(1, 5);
        switch(chance)
        {
            case 1:
                {

                    player.SetReverseControl(); 
                    break;
                }
            case 2:
                {
                    player.SetSideAttack();
                    break;
                }
            case 3:
                {
                    dartsScript.EnableFallingDarts();
                    break;
                }
            case 4:
                {
                    EnemyCenter.Instance.EnableShooting();
                    break;
                }
            default:
                {
                    AdPanel.gameObject.SetActive(true);
                    adCount = AdTime;
                    break;
                }
        }
    }
}
