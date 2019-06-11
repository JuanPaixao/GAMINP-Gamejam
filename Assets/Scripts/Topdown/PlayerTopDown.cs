using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private float _movHor, _movVer;
    private Animator _animator;
    private bool _isMoving;
    private Vector2 _lastMove;
    public bool isAttacking;
    public float resetAttack, attackCooldown;
    [SerializeField] private AnimatorClipInfo[] _animatorInfo;
    public string clipName;
    [SerializeField] private GameObject[] _hitBoxes;
    [SerializeField] private float _knockback;
    private UIManager _uiManager;
    [SerializeField] private int _playerHP;
    public int keyQuantity;
    public GameObject[] keysInventory;
    [SerializeField] private float _invencibilityCurrentTime, _invencibilityTime;
    private GameManager _gameManager;
    private CapsuleCollider2D _collider2D;
    private AudioSource _audioSource;
    public AudioClip doorSound, swordSound, damageSound;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _collider2D = GetComponent<CapsuleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        attackCooldown = resetAttack;
    }

    private void Update()
    {
        _invencibilityCurrentTime = _invencibilityCurrentTime - Time.deltaTime;
        _animatorInfo = this._animator.GetCurrentAnimatorClipInfo(0);
        clipName = _animatorInfo[0].clip.name;
        if (isAttacking)
        {
            //animation check
            _animatorInfo = this._animator.GetCurrentAnimatorClipInfo(0);
            clipName = _animatorInfo[0].clip.name;


            if (clipName == "TopDownPlayerAttackDown")
            {
                _hitBoxes[3].SetActive(true);
            }
            else if (clipName == "TopDownPlayerAttackLeft")
            {
                _hitBoxes[1].SetActive(true);
            }
            else if (clipName == "TopDownPlayerAttackRight")
            {
                _hitBoxes[2].SetActive(true);
            }
            else if (clipName == "TopDownPlayerAttackUp")
            {
                _hitBoxes[0].SetActive(true);
            }

            //animation check
            attackCooldown -= Time.deltaTime;
            if (attackCooldown < 0)
            {
                FinishedAttack();
                attackCooldown = resetAttack;
            }
        }
        else
        {
            _hitBoxes[0].SetActive(false);
            _hitBoxes[1].SetActive(false);
            _hitBoxes[2].SetActive(false);
            _hitBoxes[3].SetActive(false);
        }
        _isMoving = false; //with it, every frame i turn it off and check if i can turn it on again when moving later -----

        _movHor = Input.GetAxisRaw("Horizontal");
        _movVer = Input.GetAxisRaw("Vertical");


        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            this.transform.Translate(new Vector2(_movHor * _speed * Time.deltaTime, 0));
            _isMoving = true; // <--- here
            _lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0); //and this makes that i always have my last movement on a vector2, that will never be 0
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            this.transform.Translate(new Vector2(0, _movVer * _speed * Time.deltaTime));
            _isMoving = true;
            _lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
        }

        _animator.SetFloat("movX", _movHor);
        _animator.SetFloat("movY", _movVer);
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetFloat("lastMoveX", _lastMove.x);
        _animator.SetFloat("lastMoveY", _lastMove.y);


        if (Input.GetButtonDown("Jump"))
        {
            if (!isAttacking)
            {
                Attack();
            }
        }
    }
    private void Attack()
    {
        _isMoving = false;
        isAttacking = true;
        _animator.SetBool("isAttacking", isAttacking);

    }
    public void FinishedAttack()
    {
        isAttacking = false;
        _animator.SetBool("isAttacking", isAttacking);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_invencibilityCurrentTime <= 0)
            {
                Damage();
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
    private void Damage()
    {
        _audioSource.PlayOneShot(damageSound);
        _playerHP--;
        _invencibilityCurrentTime = _invencibilityTime;
        _uiManager.SetHPSlider(_playerHP);
        if (_playerHP <= 0)
        {
            _animator.SetTrigger("Dead");
            _collider2D.enabled = false;
        }
    }
    public void Reset()
    {
        _gameManager.RestartScene("TopDown");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (keyQuantity > 0)
        {
            if (other.CompareTag("Door"))
            {
                keyQuantity--;
                _audioSource.PlayOneShot(doorSound);
                other.gameObject.SetActive(false);

                if (this.keyQuantity == 0)
                {
                    keysInventory[0].SetActive(false);
                    keysInventory[1].SetActive(false);
                    keysInventory[2].SetActive(false);
                }
                else if (this.keyQuantity == 1)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(false);
                    keysInventory[2].SetActive(false);
                }
                else if (this.keyQuantity == 2)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(true);
                    keysInventory[2].SetActive(false);
                }
                else if (this.keyQuantity == 3)
                {
                    keysInventory[0].SetActive(true);
                    keysInventory[1].SetActive(true);
                    keysInventory[2].SetActive(true);
                }
            }
        }
        if (other.gameObject.CompareTag("Stair"))
        {
            _gameManager.LoadSceneWithDealy();
            this._speed = 0;
            SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }
    }
    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(swordSound);
    }
}
