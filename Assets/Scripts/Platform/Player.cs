using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed, _jumpForce, _circleSphereRay;
    [SerializeField] private string _direction;
    public bool isJumping, isMoving, isGrounded, isDead;
    private float _movement, _movX;
    [SerializeField] private LayerMask _layerMaskGround, _layerMaskToEnemies;
    [SerializeField] private Transform _playerBottom;
    [SerializeField] private RaycastHit2D _hit;
    [SerializeField] private float _raycastSize;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimations _playerAnimator;
    private float _jumpSpeed;
    public int HP;
    private UIManager _uiManager;
    [SerializeField] private float _invencibilityCurrentTime, _invencibilityTime;
    private GameManager _gameManager;
    private bool _invencible;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _knockback;
    public AudioClip jump;
    public GameObject musicSourceBoss;
    private AudioSource _audioSource;
    public AudioSource stopMusic;
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<PlayerAnimations>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _jumpSpeed = _speed / 1.5f;
    }
    void Update()
    {
        if (!isDead)
        {
            Move();
            AnimationCheck();
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            _rb.velocity = new Vector2(_movement, this._rb.velocity.y);
        }
        CheckGround();
        _invencibilityCurrentTime = _invencibilityCurrentTime - Time.deltaTime;
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
            _audioSource.PlayOneShot(jump);
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
                Enemy enemy = _hit.collider.GetComponent<Enemy>();
                enemy.TakeDamage(1);
                _rb.velocity = new Vector2(this._rb.velocity.x, 5f);
                _particle.Play();
            }
            else if (_hit.collider.CompareTag("MushroomJump"))
            {
                _rb.velocity = new Vector2(this._rb.velocity.x, 10f);
                _particle.Play();
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
        if (_invencibilityCurrentTime <= 0)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                TakeDamage();

                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (_invencibilityCurrentTime <= 0)
                    {
                        if (other.gameObject.transform.position.x < this.transform.position.x)
                        {
                            transform.Translate(Vector2.right * _knockback * Time.deltaTime);

                        }
                        if (other.gameObject.transform.position.x > this.transform.position.x)
                        {
                            transform.Translate(Vector2.left * _knockback * Time.deltaTime);

                        }
                        if (other.gameObject.transform.position.y < this.transform.position.y)
                        {
                            transform.Translate(Vector2.up * _knockback * Time.deltaTime);

                        }
                        if (other.gameObject.transform.position.y > this.transform.position.y)
                        {
                            transform.Translate(Vector2.down * _knockback * Time.deltaTime);

                        }
                    }
                }
            }
        }
    }
    private void AnimationCheck()
    {
        _playerAnimator.isMoving(isMoving);
        _playerAnimator.isJumping(isJumping);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "cloudAttack")
        {
            if (_invencibilityCurrentTime <= 0)
            {
                TakeDamage();
            }
        }
        else if (other.gameObject.name == "DeadWall")
        {
            isDead = true;
            StartCoroutine(RestartLevel());
        }
        else if (other.gameObject.name == "ChangeMyMusic")
        {
            this.stopMusic.volume =0;
            musicSourceBoss.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
    private void TakeDamage()
    {
        this.HP--;
        _invencibilityCurrentTime = _invencibilityTime;
        _uiManager.SetSpriteHP(HP);

        if (HP <= 0)
        {
            _playerAnimator.isDead();
            isDead = true;
            StartCoroutine(RestartLevel());
        }
        else
        {
            StartCoroutine(DamageRoutine());

        }
    }
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(2.25f);
        _gameManager.RestartScene("Platform");
    }
    private IEnumerator DamageRoutine()
    {
        _invencible = true;
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
        yield return new WaitForSecondsRealtime(1f);
        _invencible = false;
    }
}
