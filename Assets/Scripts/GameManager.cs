using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string sceneName;
    public float shooterTimeBoss;
    public UIManager uiManager;
    public GameObject boss;
    public GameObject explosion;
    public GameObject MenuUI, Dialog;
    private bool _started;
    public bool withBoss;
    private AudioSource _audioSource;
    public static string language;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (this.sceneName == "Credits")
        {
            StartCoroutine(LoadMenu());
        }
        if (this.sceneName == "LanguageSelection")
        {
            language = null;
        }
    }
    private void Update()
    {
        if (!_started)
        {
            if (Input.anyKeyDown)
            {
                if (this.sceneName == "Menu")
                {
                    MenuUI.SetActive(false);
                    StartCoroutine(StartDialog());
                }
            }
        }
        shooterTimeBoss -= Time.deltaTime;
        if (shooterTimeBoss < 0)
        {
            ActiveBossShip();
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    public void LoadSceneWithDealy()
    {
        StartCoroutine(SceneDelay());
    }
    public void RestartScene(string sceneNameRestart)
    {
        SceneManager.LoadSceneAsync(sceneNameRestart, LoadSceneMode.Single);
    }

    public void ActiveBossShip()
    {
        if (withBoss)
        {
            boss.SetActive(true);
        }
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
    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(0.5f);
        Dialog.SetActive(true);
    }
    private IEnumerator SceneDelay()
    {
        yield return new WaitForSeconds(2f);
        LoadScene(sceneName);
    }
    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(10f);
        LoadScene("Menu");
    }
    public void SetLanguage(string setLang)
    {
        language = setLang;
        LoadScene("Menu");
    }
    public string GetLanguage()
    {
        return language;
    }
}