using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _rain;
    private Transform _player;
    private float _distance;
    private bool _rainRoutineControl;
    private Rigidbody2D _rainRB;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        _rain.transform.position = new Vector2(this.transform.position.x - 0.064f, this.transform.position.y - 0.254f);
        _rain.SetActive(false);
        _rainRB = _rain.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _distance = Vector2.Distance((Vector2)_player.position, (Vector2)this.transform.position);

        if (_distance < 9)
        {
            RainAnimation();
            if (!_rainRoutineControl)
            {
                StartCoroutine(RainRoutine());
            }
        }
        if (_distance >= 9)
        {
            _rain.SetActive(false);
            _rain.transform.position = new Vector2(this.transform.position.x - 0.064f, this.transform.position.y - 0.254f);
            _rainRoutineControl = false;
        }
    }
    private IEnumerator RainRoutine()
    {
        _rainRoutineControl = true;
        while (_distance < 9)
        {
            yield return new WaitForSecondsRealtime(3);
            RainAnimation();
        }
    }
    private void RainAnimation()
    {
        _animator.SetTrigger("Rain");
    }
    public void MakeItRain()
    {
        _rain.SetActive(true);
        _rain.transform.position = new Vector2(this.transform.position.x - 0.064f, this.transform.position.y - 0.254f);
        _rainRB.velocity = Vector2.zero;
    }
}
