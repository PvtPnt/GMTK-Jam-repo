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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomStatus()
    {
        currStatus = Statuses[0];
        player.SetSideAttack(true);
    }
}
