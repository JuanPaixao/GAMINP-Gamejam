using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _direction;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }
    private void EnemyMovement()
    {
        if (_direction == "left")
        {
            _rb.velocity = new Vector2(-_speed * Time.deltaTime, this._rb.velocity.y);
        }
        if (_direction == "right")
        {
            _rb.velocity = new Vector2(_speed * Time.deltaTime, this._rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ChangeDirectionWall"))
        {
            if (_direction == "left")
            {
                _direction = "right";
            }
            else
            {
                _direction = "left";
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_direction == "left")
            {
                _direction = "right";
            }
            else
            {
                _direction = "left";
            }
        }
    }
}
