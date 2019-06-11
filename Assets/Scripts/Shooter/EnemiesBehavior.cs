using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _initialPos;
    [SerializeField] private float _amplitude;
    private float _angle;
    public int enemyType;
    [SerializeField] private GameObject _laser;
    [SerializeField] private float _laserCooldown;
    [SerializeField] private GameObject[] destroyRoutine;
    private GameManager _gameManager;
    public bool hitted;
    


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        this._initialPos = this.transform.position;

    }
    private void Start()
    {
        if (!this.CompareTag("Upgrade"))
        {
            hitted = false;
            InvokeRepeating("Shoot", 1f, _laserCooldown);
        }
    }
    void Update()
    {

        this.transform.Translate(0, -_speed * Time.deltaTime, 0);
        if (this.enemyType == 1)
        {
            SineMovement();
        }
        if (this.transform.position.y < -10f)
        {
            OutOfBounds();
        }
        if (_gameManager.shooterTimeBoss <= 0)
        {
            if (!this.gameObject.CompareTag("Upgrade"))
            {
                DestroyMe();
            }
        }

    }
    private void Shoot()
    {
        if (hitted == false)
        {
            if (_gameManager.shooterTimeBoss >= 0)
            {
                Instantiate(_laser, this.transform.position, Quaternion.identity); //placeholder to memory stack later
            }
        }
    }

    private void SineMovement()
    {
        this._angle += this._speed * Time.deltaTime; //increasing angle by one and using sine on it, so it will make the sine goes between -1 and 1
        float variation = Mathf.Sin(_angle); //period
        this.transform.position = this.transform.position + (this._amplitude * variation * Vector3.right);
    }
    public void OutOfBounds()
    {
        EnemiesBackup returnMe = GameObject.FindObjectOfType<EnemiesBackup>().GetComponent<EnemiesBackup>();
        returnMe.StackEnemy(this.gameObject);
        if (!this.CompareTag("Upgrade"))
        {
            destroyRoutine[1].SetActive(false);
            destroyRoutine[0].SetActive(true);
        }
    }
    public void DestroyMe()
    {
        hitted = true;
        destroyRoutine[0].SetActive(false);
        destroyRoutine[1].SetActive(true);
        if (_gameManager.shooterTimeBoss > 0)
        {
            StartCoroutine(ReturnRoutine());
        }
        else
        {
            OutOfBounds();
        }
    }

    public void WhenCreated()
    {
        destroyRoutine[1].SetActive(false);
        destroyRoutine[0].SetActive(true);
    }
    private IEnumerator ReturnRoutine()
    {
        yield return new WaitForSeconds(0.45f);
        OutOfBounds();
    }
}
