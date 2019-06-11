using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesShoot : MonoBehaviour
{
    [SerializeField] private float _shootSpeed;
    private Animator _shootAnimator;
    private Transform _player;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<ShooterPlayer>().GetComponent<Transform>();
        _shootAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(DestroyMe());
        float distanceTest = Vector2.Distance(_player.position, this.transform.position);
        if (distanceTest < 12f)
        {
            _audioSource.Play();
        }
    }
    private void Update()
    {
        this.transform.Translate(new Vector2(0, -_shootSpeed * Time.deltaTime));
    }
    private void ShootHit()
    {
        _shootAnimator.SetTrigger("isDestroying");
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShootHit();
            this._shootSpeed /= 3;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
