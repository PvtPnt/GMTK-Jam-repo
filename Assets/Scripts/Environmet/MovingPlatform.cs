using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform markerA;
    public Transform markerB;
    Vector3 currentTarget;
    Vector3 TargetPosition_A;
    Vector3 TargetPosition_B;
    public float moveSpeed;
    public bool movetoA = true;

    // Start is called before the first frame update
    void Start()
    {
        TargetPosition_A = markerA.position;
        TargetPosition_B = markerB.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Assign target to move to
        if (movetoA)
        {
            currentTarget = TargetPosition_A;
        }
        else
        {
            currentTarget = TargetPosition_B;
        }

        // Moving platform itself
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed);

        // Reassign target when reachign destination
        if (transform.position == currentTarget)
        {
            switch(movetoA)
            {
                case true: movetoA = false;
                    break;

                case false: movetoA = true;
                    break;
            }    
        }
    }
}
