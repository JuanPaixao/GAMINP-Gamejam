using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _direction;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    public float hp;
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;
    private Animator _animator;
    public AudioSource audioSource;
    public AudioClip bossHit, goombSound, winSound;
    public GameObject stopAllMusic;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        int random = Random.Range(0, 2);
        _direction = random == 0 ? "left" : "right";
    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }
    public void PlayVictoySound()
    {
        stopAllMusic.SetActive(false);
        audioSource.PlayOneShot(winSound, .5f);
    }
    private void EnemyMovement()
    {
        if (_direction == "left")
        {
            _rb.velocity = new Vector2(-_speed * Time.deltaTime, this._rb.velocity.y);
            _spriteRenderer.flipX = true;
        }
        if (_direction == "right")
        {
            _rb.velocity = new Vector2(_speed * Time.deltaTime, this._rb.velocity.y);
            _spriteRenderer.flipX = false;
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
    public void TakeDamage(int damage)
    {
        if (this.gameObject.name != "Boss")
        {
            audioSource.PlayOneShot(goombSound);
        }
        hp -= damage;
        StartCoroutine(DamageRoutine());
        if (hp <= 0)
        {
            if (this.gameObject.name != "Boss")
            {
                this.gameObject.SetActive(false);
                _speed = 0;
            }
            else
            {
                _animator.SetTrigger("Dead");
            }
        }

        else if (this.gameObject.name == "Boss")
        {
            if (this.hp == 5)
            {
                audioSource.PlayOneShot(bossHit);
                this._spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g - 169, this._spriteRenderer.color.b - 169, 1);
                _speed = _speed * 1.65f;
            }

        }
    }
    private IEnumerator DamageRoutine()
    {
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
    }
    public void NextStage()
    {
        _gameManager.LoadScene("Transition_Platform");
    }
}
