using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShipCannons : MonoBehaviour
{
    public int hpCannon;
    public Animator animator;
    [SerializeField] private GameObject _bossShot;
    public bool destroyed;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private int _id; //if One, can be destroyed;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _bossObject;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _invencibilityCurrentTime, _invencibilityTime;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        _invencibilityCurrentTime = _invencibilityCurrentTime - Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this._id == 1)
        {
            if (other.gameObject.CompareTag("Shoot"))
            {
                this.hpCannon--;

                if (hpCannon <= 0)
                {
                    animator.SetTrigger("Destroyed");
                    destroyed=true;
                }
            }
        }
        else if (this._id == 2)
        {
            if (other.gameObject.CompareTag("Shoot"))
            {
                if (_invencibilityCurrentTime <= 0)
                {
                    this.hpCannon--; 
                    _invencibilityCurrentTime = _invencibilityTime;
                }

                if (this.hpCannon <= 0)
                {
                    Dead();
                }
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 3f, _shootCooldown);
    }

    private void Shoot()
    {
        if (!destroyed)
        {
            Instantiate(_bossShot, this.transform.position, Quaternion.identity); //placeholder to memory stack later
        }
    }
    private void Dead()
    {
        _gameManager.DeactiveBossShip(this.transform);
        _gameManager.LoadSceneWithDealy();
        _bossObject.SetActive(false);
    }
}
