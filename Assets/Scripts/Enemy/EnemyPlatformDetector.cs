using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatformDetector : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ground") {
            enemy.Turn();
        }
    }
}
