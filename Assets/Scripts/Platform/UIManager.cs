using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _gameObject;


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
}
