using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActivate;
    [SerializeField] Material m_activated;
    // Start is called before the first frame update
    void Start()
    {
        isActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivate == false && other.gameObject.tag == "Player")
        {
            isActivate = true;
            gameObject.GetComponent<Renderer>().material = m_activated;
        }
    }
}
