using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStatus : MonoBehaviour
{
    public static RandomStatus SharedInstance;
    string currStatus;
    [SerializeField] BouncerController player;
    List<string> Statuses;
    // Start is called before the first frame update

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        Statuses = new List<string>();
        Statuses.Add("SideAttack");
        Statuses.Add("ReverseControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomStatus()
    {
        if(Random.Range(1, 10) < 6)
        {
            currStatus = Statuses[1];
            player.SetReverseControl();
        }
        else
        {
            currStatus = Statuses[1];
            player.SetSideAttack();
        }

        //player.SetSideAttack(true);
    }
}
