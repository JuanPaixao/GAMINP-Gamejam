using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string sceneName;
    public int shooterScore;
    public UIManager uiManager;
    public GameObject boss;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    public void RestartScene(string sceneNameRestart)
    {
        SceneManager.LoadSceneAsync(sceneNameRestart, LoadSceneMode.Single);
    }
    public void IncreaseScore()
    {
        shooterScore++;
        SetText(shooterScore);
    }
    public void SetText(int shooterScore)
    {
        shooterScore = 60 - shooterScore; //60 as placeholder for max score
        if (shooterScore < 0)
        {
            shooterScore = 0;
        }
        uiManager.SetText(shooterScore.ToString());
    }
    public void ActiveBossShip()
    {
        boss.SetActive(true);
    }
}