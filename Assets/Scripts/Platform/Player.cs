using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed, _jumpForce, _circleSphereRay;
    [SerializeField] private string _direction;
    public bool isJumping, isMoving, isGrounded;
    private float _movement, _movX;
    [SerializeField] private LayerMask _layerMaskGround, _layerMaskToEnemies;
    [SerializeField] private Transform _playerBottom;
    [SerializeField] private RaycastHit2D _hit;
    [SerializeField] private float _raycastSize;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimations _playerAnimator;
    private float _jumpSpeed;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<PlayerAnimations>();
    }
    private void Start()
    {
        _jumpSpeed = _speed / 1.5f;
    }
    void Update()
    {
        Move();
        AnimationCheck();
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            _rb.velocity = new Vector2(_movement, this._rb.velocity.y);
        }
        CheckGround();
    }

    private void Move()
    {
        _movX = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {
            _movement = _movX * _speed * Time.fixedDeltaTime;
        }
        if (isJumping)
        {
            _movement = _movX * _jumpSpeed * Time.fixedDeltaTime;
        }
        if (_movX > 0)
        {
            _direction = "Right";
        }
        else if (_movX < 0)
        {
            _direction = "Left";
        }

        isMoving = _movX != 0 ? true : false;
        _spriteRenderer.flipX = _direction == "Right" ? false : true;
        isJumping = _rb.velocity.y != 0f ? true : false;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector2.up * Mathf.Sqrt(_jumpForce * -1 * Physics.gravity.y), ForceMode2D.Impulse);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (_rb.velocity.y > 0)
            {
                this._rb.velocity = new Vector2(this._rb.velocity.x, this._rb.velocity.y * 0.5f);
            }
        }
    }
    private void CheckGround()
    {
        isGrounded = (Physics2D.OverlapCircle((Vector2)this._playerBottom.position, _circleSphereRay, _layerMaskGround));
        _hit = Physics2D.Raycast(this._playerBottom.position, Vector2.down, _raycastSize, _layerMaskToEnemies);
        if (_hit.collider != null)
        {
            if (_hit.collider.CompareTag("Enemy"))
            {
                Destroy(_hit.collider.gameObject);
                _rb.velocity = new Vector2(this._rb.velocity.x, 5f);
            }
            else if (_hit.collider.CompareTag("MushroomJump"))
            {
                _rb.velocity = new Vector2(this._rb.velocity.x, 10f);
                Debug.Log("mushroom");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_playerBottom.position, _circleSphereRay);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("take damage");
        }
    }
    private void AnimationCheck()
    {
        _playerAnimator.isMoving(isMoving);
        _playerAnimator.isJumping(isJumping);
    }

}
