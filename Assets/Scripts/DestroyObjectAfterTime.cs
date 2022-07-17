using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    [SerializeField] private float timeOut;

    // Update is called once per frame
    void Update()
    {
        timeOut -= Time.deltaTime;
        if (timeOut <= 0) Destroy(gameObject);
    }
}
