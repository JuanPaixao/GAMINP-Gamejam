using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootMechanic : MonoBehaviour
{
    [SerializeField] private float _shootSpeed;
    private Animator _shootAnimator;
    [SerializeField] UnityEvent _CollisionEvent;


    private void Awake()
    {
        _shootAnimator = GetComponent<Animator>();

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
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _CollisionEvent.Invoke();
            other.gameObject.GetComponent<EnemiesBehavior>().DestroyMe();
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