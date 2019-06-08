using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesBehaviour : MonoBehaviour
{

    [SerializeField] private float _viewDistance, _distance, _slimeSpeed;
    private Transform _player;
    private Animator _animator;
    private bool _isDead;
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        _distance = Vector2.Distance((Vector2)_player.position, (Vector2)this.transform.position);
        if (_distance <= _viewDistance)
        {
            if (!_isDead)
            {
                Vector2 direction = this._player.position - this.transform.position;
                transform.Translate(direction.normalized * _slimeSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            _animator.SetTrigger("isDead");
            _isDead = true;
        }
    }
    public void Dead()
    {
        this.gameObject.SetActive(false);
    }
}
