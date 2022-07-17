using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NormalTimescale()
    {
        Time.timeScale = 1;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleActived(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SetActiveStateT(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void SetActiveStateF(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
