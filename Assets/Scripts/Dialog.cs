using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentencesPT;
    private int index;
    public float typeSpeed;
    public Animator textDisplayAnimator;
    private AudioSource source;
    private GameManager _gameManager;
    public string sceneToLoad;
    void Start()
    {
        StartCoroutine(TypeRoutine());
        source = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == sentencesPT[index])
        {
            if (Input.GetButtonDown("Jump"))
            {
                NextSentence();
            }
        }
    }
    private IEnumerator TypeRoutine()
    {
        textDisplayAnimator.SetTrigger("Change");
        foreach (char letter in sentencesPT[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
    public void NextSentence()
    {
        source.Play();
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
}
