using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCenter : MonoBehaviour
{
    public static EnemyCenter Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void EnableShooting()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Enemy>().EnableShooting();
        }
    }

}
