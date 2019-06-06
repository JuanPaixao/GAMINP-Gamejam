using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _isMoving;
    private ShooterPlayerAnimations _shooterPlayerAnim;
    [SerializeField] private GameObject _laser;
    [SerializeField] private int _upgrade;
    private void Awake()
    {
        _shooterPlayerAnim = GetComponent<ShooterPlayerAnimations>();
    }

    void Update()
    {
        MoveShip();
        AttackShip();
    }

    private void AttackShip()
    {
        if (Input.GetButton("Jump"))
        {
            if (_upgrade == 0)
            {
                //shoot0
            }
            else if (_upgrade == 1)
            {
                //
            }
            else if (_upgrade == 2)
            {
                //
            }
            else
            {
                //
            }
        }
    }

    public void MoveShip()
    {
        float movHor = Input.GetAxis("Horizontal");
        float movVer = Input.GetAxis("Vertical");

        if (movHor != 0 || movVer != 0)
        {
            _isMoving = true;
            _shooterPlayerAnim.isMoving(true);
        }
        else
        {
            _isMoving = false;
            _shooterPlayerAnim.isMoving(false);
        }

        transform.Translate(new Vector2(this._speed * movHor, this._speed * movVer) * Time.deltaTime);

        if (this.transform.position.x <= -9.78f)
        {
            this.transform.position = new Vector2(-9.78f, this.transform.position.y);
        }
        if (this.transform.position.x >= 9.78f)
        {
            this.transform.position = new Vector2(9.78f, this.transform.position.y);
        }

        if (this.transform.position.y >= 4.764451f)
        {
            this.transform.position = new Vector2(this.transform.position.x, 4.764451f);
        }
        if (this.transform.position.y <= -4.764451f)
        {
            this.transform.position = new Vector2(this.transform.position.x, -4.764451f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Upgrade"))
        {
            if (_upgrade < 4)
            {
                _upgrade++;
            }
        }
    }
}
