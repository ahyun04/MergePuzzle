using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    void Update()
    {

    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("GameHome");
    }
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("Á¾·á");
    }
    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void OnClickStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void OnClickGameHomeExit()
    {
        SceneManager.LoadScene("Main");
    }
}
