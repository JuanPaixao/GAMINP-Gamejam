using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _movement;
    private Rigidbody _rb;
    private float _movHor, _movVer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        _movHor = Input.GetAxisRaw("Horizontal");
        _movVer = Input.GetAxisRaw("Vertical");

        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _movement = new Vector3(this._movHor, 0, this._movVer);
        _movement *= _speed * Time.deltaTime;
        _rb.MovePosition(this.transform.position + _movement);
    }
}
