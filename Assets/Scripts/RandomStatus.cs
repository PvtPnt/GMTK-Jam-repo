using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStatus : MonoBehaviour
{
    List<string> Statuses;
    // Start is called before the first frame update
    void Start()
    {
        Statuses.Add("SideAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetRandomStatus()
    {
        return Statuses[0];
    }
}
