using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform positionTeleport;
    public float teleportPosX, teleportPosY;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Start()

    {
        Vector2 teleportLocation = positionTeleport.position;
        teleportPosX = teleportLocation.x;
        teleportPosY = teleportLocation.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = new Vector2(teleportPosX, teleportPosY);
            _audioSource.Play();
        }
    }
}
