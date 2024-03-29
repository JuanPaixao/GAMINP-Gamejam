﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _gameObject;
    public Text UIText;
    public Slider slider;


    public void SetSpriteHP(int hp)
    {
        if (hp == 3)
        {
            _gameObject[0].SetActive(true);
            _gameObject[1].SetActive(true);
            _gameObject[2].SetActive(true);
        }
        else if (hp == 2)
        {
            _gameObject[0].SetActive(true);
            _gameObject[1].SetActive(true);
            _gameObject[2].SetActive(false);
        }
        else if (hp == 1)
        {
            _gameObject[0].SetActive(true);
            _gameObject[1].SetActive(false);
            _gameObject[2].SetActive(false);
        }
        else
        {
            _gameObject[0].SetActive(false);
            _gameObject[1].SetActive(false);
            _gameObject[2].SetActive(false);
        }
    }
    public void SetText(string text)
    {
        this.UIText.text = text;
    }
    public void SetHPSlider(int hp)
    {
        this.slider.value = hp;
    }
}
