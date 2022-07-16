using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Color loadColor;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(string LevelName)
    {
        Initiate.Fade(LevelName, loadColor, Speed);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
