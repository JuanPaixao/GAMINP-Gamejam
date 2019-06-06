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

    void Awake()
    {
        this._initialPos = this.transform.position;
    }
    private void Start()
    {
        InvokeRepeating("Shoot", 3f, _laserCooldown);
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
    }
    private void Shoot()
    {
        Instantiate(_laser, this.transform.position, Quaternion.identity); //placeholder to memory stack later
    }
    private void SineMovement()
    {
        this._angle += this._speed * Time.deltaTime; //increasing angle by one and using sine on it, so it will make the sine goes between -1 and 1
        float variation = Mathf.Sin(_angle); //period
        this.transform.position = this.transform.position + (this._amplitude * variation * Vector3.right);
    }
    private void OutOfBounds()
    {
        EnemiesBackup returnMe =  GameObject.FindObjectOfType<EnemiesBackup>().GetComponent<EnemiesBackup>();
        returnMe.StackEnemy(this.gameObject);
    }
}
