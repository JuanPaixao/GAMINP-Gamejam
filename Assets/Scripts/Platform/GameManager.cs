using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string sceneName;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    public void RestartScene(string sceneNameRestart)
    {
        SceneManager.LoadSceneAsync(sceneNameRestart, LoadSceneMode.Single);
    }
}
