using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentencesPT, sentencesENG;
    private int index;
    public float typeSpeed;
    public Animator textDisplayAnimator;
    private AudioSource source;
    private GameManager _gameManager;
    public string sceneToLoad;
    public static string language;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
        this.SetLanguage();
    }
    void Start()
    {
        StartCoroutine(TypeRoutine());
        Debug.Log(language);
    }

    // Update is called once per frame
    void Update()
    {
        if (language == "Portuguese")
        {
            if (textDisplay.text == sentencesPT[index])
            {
                if (Input.GetButtonDown("Jump"))
                {
                    NextSentence();
                }
            }
        }
        else if (language == "English")
        {
            if (textDisplay.text == sentencesENG[index])
            {
                if (Input.GetButtonDown("Jump"))
                {
                    NextSentence();
                }
            }
        }
    }
    private IEnumerator TypeRoutine()
    {
        textDisplayAnimator.SetTrigger("Change");
        if (language == "Portuguese")
        {
            foreach (char letter in sentencesPT[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
        }
        else if (language == "English")
        {
            foreach (char letter in sentencesENG[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
        }

    }
    public void NextSentence()
    {
        source.Play();
        if (language == "Portuguese")
        {
            if (index < sentencesPT.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(TypeRoutine());
            }
            else
            {
                textDisplay.text = "";
                if (_gameManager != null)
                {
                    _gameManager.LoadScene(sceneToLoad);
                }
            }
        }

        else if (language == "English")
        {
            if (index < sentencesENG.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(TypeRoutine());
            }
            else
            {
                textDisplay.text = "";
                if (_gameManager != null)
                {
                    _gameManager.LoadScene(sceneToLoad);
                }
            }
        }
    }

    public void SetLanguage()
    {
        language = _gameManager.GetLanguage();
    }
}
