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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (this._id == 1)
        {
            if (other.gameObject.CompareTag("Shoot"))
            {
                this.hpCannon--;
                if (hpCannon <= 0)
                {

                    animator.SetTrigger("Destroyed");
                }
            }
        }
        else if (this._id == 2)
        {
            if (other.gameObject.CompareTag("Shoot"))
            {
                this.hpCannon--;
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
}
