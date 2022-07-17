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
    // Start is called before the first frame update

    void Awake()
    {
        SharedInstance = this;
    }

    public void GetRandomStatus()
    {
        int chance = Random.Range(1, 6);
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
                    player.SetDarts();
                    dartsScript.EnableFallingDarts();
                    break;
                }
            case 4:
                {
                    player.SetGodMode();
                    break;
                }
            default:
                {
                    player.SetAds();
                    break;
                }
        }
    }
}
