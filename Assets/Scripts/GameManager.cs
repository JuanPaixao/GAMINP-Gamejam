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
    public GameObject explosion;

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
    public void DeactiveBossShip(Transform pos)
    {
        StartCoroutine(ExplosionRoutine(pos));
    }
    private IEnumerator ExplosionRoutine(Transform pos)
    {
        var exp1 = Instantiate(explosion, new Vector2(pos.transform.position.x, pos.transform.position.y), Quaternion.identity);
        exp1.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        exp1.SetActive(false);

        var exp2 = Instantiate(explosion, new Vector2(pos.transform.position.x + 2, pos.transform.position.y - 1), Quaternion.identity);
        exp2.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        exp2.SetActive(false);

        var exp3 = Instantiate(explosion, new Vector2(pos.transform.position.x - 1, pos.transform.position.y + 1), Quaternion.identity);
        exp3.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        exp3.SetActive(false);

    }
}