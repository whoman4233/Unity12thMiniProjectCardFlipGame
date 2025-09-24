using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void Retry()
    {
        SceneManager.LoadScene("MainScene 1");
    }
}