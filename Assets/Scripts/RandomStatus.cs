using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomStatus : MonoBehaviour
{
    public static RandomStatus SharedInstance;
    string currStatus;
    [SerializeField] BouncerController player;
    [SerializeField] Image AdPanel;
    [SerializeField] float AdTime = 10;
    float adCount = 0;
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
        Statuses.Add("Adcertisement");
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
        int chance = Random.Range(3, 3);
        if (chance == 1)
        {
            currStatus = Statuses[0];
            player.SetReverseControl();
        }
        else if (chance == 2)
        {
            currStatus = Statuses[1];
            player.SetSideAttack();
        }
        else
        {
            currStatus = Statuses[2];
            AdPanel.gameObject.SetActive(true);
            adCount = AdTime;
        }

    }
}
