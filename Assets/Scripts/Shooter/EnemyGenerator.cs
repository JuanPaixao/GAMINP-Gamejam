using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private Rect _area;
    [SerializeField] private EnemiesBackup _enemiesBackup;
    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    private void Start()
    {
        InvokeRepeating("Instantiate", 0, this._time);
    }
    private void Instantiate()
    {
        if (_gameManager.shooterScore <= 60)
        {
            if (this._enemiesBackup.HasEnemy())
            {
                GameObject enemy = this._enemiesBackup.UnstackEnemy();
                this.SetEnemyPosition(enemy);
            }
        }
    }

    private void SetEnemyPosition(GameObject enemy)
    {
        Vector2 randomPos = new Vector2
         (UnityEngine.Random.Range(this._area.x, this._area.x + this._area.width),
         UnityEngine.Random.Range(this._area.y, this._area.y + this._area.y + this._area.height)); // the spawn pos will cycle between my X and my total between X + any value until my total width, same for Y
        Vector2 spawnPosition = (Vector2)this.transform.position + randomPos;
        enemy.transform.position = spawnPosition;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(100, 0, 100);
        Vector2 position = this._area.position + (Vector2)this.transform.position + this._area.size / 2;
        Gizmos.DrawWireCube(position, this._area.size);
    }
}
