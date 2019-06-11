using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool open;
    private Animator _animator;
    public GameObject[] keysInventory;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!open)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                PlayChestSound();
                _animator.SetTrigger("Open");
                PlayerTopDown playerTopDown = other.GetComponent<PlayerTopDown>();
                playerTopDown.keyQuantity++;
                open = true;

                if (playerTopDown.keyQuantity == 0)
                {
                    keysInventory[0].SetActive(false);
                    keysInventory[1].SetActive(false);
                    keysInventory[2].SetActive(false);
                }
                else if (playerTopDown.keyQuantity == 1)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(false);
                    keysInventory[2].SetActive(false);
                }
                else if (playerTopDown.keyQuantity == 2)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(true);
                    keysInventory[2].SetActive(false);
                }
                else if (playerTopDown.keyQuantity == 3)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(true);
                    keysInventory[2].SetActive(true);
                }
            }
        }
    }
    public void PlayChestSound()
    {
        _audioSource.Play();
    }
}
