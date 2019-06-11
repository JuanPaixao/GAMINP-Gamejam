using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootMechanic : MonoBehaviour
{
    [SerializeField] private float _shootSpeed;
    private Animator _shootAnimator;
    [SerializeField] UnityEvent _CollisionEvent;
    private Collider2D _collider;

    private void Awake()
    {
        _shootAnimator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        StartCoroutine(DestroyMe());
    }
    private void Update()
    {
        this.transform.Translate(new Vector2(0, _shootSpeed * Time.deltaTime));
    }
    public void ShootHit()
    {
        _shootAnimator.SetTrigger("isDestroying");
        Debug.Log("Teste");
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemiesBehavior enemy = other.GetComponent<EnemiesBehavior>();
            _CollisionEvent.Invoke();
            if (enemy != null)
            {
                enemy.DestroyMe();
            }
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