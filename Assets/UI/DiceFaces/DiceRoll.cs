using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public int TargetFace;
    BouncerController player;
    Animator animator;
    Image selfFace;
    [SerializeField] Sprite[] DiceFaces;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<BouncerController>();
        selfFace = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
}
