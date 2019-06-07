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
    [SerializeField] private Transform[] _cannonsPosition;
    [SerializeField] private float _cooldownShoot, _actualCooldownShoot;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField] private float _invencibilityCurrentTime, _invencibilityTime;
    public float limitY, limitX;
    public bool imDead;
    public GameObject explosion;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _shooterPlayerAnim = GetComponent<ShooterPlayerAnimations>();

    }

    void Update()
    {
        if (!imDead)
        {
            MoveShip();
            AttackShip();
            _actualCooldownShoot -= Time.deltaTime;
            _invencibilityCurrentTime -= Time.deltaTime;
        }
        else
        {
            this._spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
            explosion.SetActive(true);
        }
    }

    private void AttackShip()
    {
        if (_actualCooldownShoot <= 0)
        {
            if (Input.GetButton("Jump"))
            {
                if (_upgrade <= 0)
                {
                    Instantiate(_laser, _cannonsPosition[0].position, Quaternion.identity);
                }
                else if (_upgrade == 1)
                {
                    Instantiate(_laser, _cannonsPosition[0].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[1].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[2].position, Quaternion.identity);

                }
                else if (_upgrade == 2)
                {
                    Instantiate(_laser, _cannonsPosition[0].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[1].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[2].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[3].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[4].position, Quaternion.identity);

                }
                else
                {
                    Instantiate(_laser, _cannonsPosition[0].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[1].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[2].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[3].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[4].position, Quaternion.identity);
                    Instantiate(_laser, _cannonsPosition[5].position, Quaternion.identity);
                }
                _actualCooldownShoot = _cooldownShoot;
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

        if (this.transform.position.x <= -limitX)
        {
            this.transform.position = new Vector2(-limitX, this.transform.position.y);
        }
        if (this.transform.position.x >= limitX)
        {
            this.transform.position = new Vector2(limitX, this.transform.position.y);
        }

        if (this.transform.position.y >= limitY)
        {
            this.transform.position = new Vector2(this.transform.position.x, limitY);
        }
        if (this.transform.position.y <= -limitY)
        {
            this.transform.position = new Vector2(this.transform.position.x, -limitY);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Upgrade"))
        {
            if (_upgrade < 3)
            {
                _upgrade++;
                _animator.SetInteger("Upgrade", _upgrade);
                other.gameObject.GetComponent<EnemiesBehavior>().OutOfBounds();
            }
            else if (_upgrade >= 3)
            {
                _upgrade = 3;
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_invencibilityCurrentTime <= 0)
            {
                if (_upgrade > 0)
                {
                    _upgrade--;
                    _animator.SetInteger("Upgrade", _upgrade);
                }
                else
                {
                    imDead = true;
                    _upgrade = 0;
                    _animator.SetInteger("Upgrade", _upgrade);
                }
                if (!imDead)
                {
                    StartCoroutine(DamageRoutine());
                }
            }
        }
    }
    private IEnumerator DamageRoutine()
    {
        _invencibilityCurrentTime = _invencibilityTime;
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 0);
        yield return new WaitForEndOfFrame();
        _spriteRenderer.color = new Color(this._spriteRenderer.color.r, this._spriteRenderer.color.g, this._spriteRenderer.color.b, 1);
    }
}
