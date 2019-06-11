using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBackup : MonoBehaviour
{
    private Stack<GameObject> _enemiesStack;
    [SerializeField] private int _quantity;
    [SerializeField] private GameObject[] _enemies;
    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        this._enemiesStack = new Stack<GameObject>();
        this.CreateAllEnemies();
    }

    internal void ReturnEnemy(GameObject gameObject)
    {
        throw new NotImplementedException();
    }
    private void CreateAllEnemies()
    {
        for (int i = 0; i < this._quantity; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, 6);
            GameObject enemy = GameObject.Instantiate(_enemies[randomNumber], this.transform);
            EnemiesBackupObject backupEnemy = enemy.GetComponent<EnemiesBackupObject>();
            backupEnemy.SetBackup(this);
            enemy.SetActive(false);
            this._enemiesStack.Push(enemy);
        }
    }
    public GameObject UnstackEnemy()
    {
        GameObject enemy = this._enemiesStack.Pop();
        enemy.SetActive(true);
        return enemy;
    }

    public void StackEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        this._enemiesStack.Push(enemy);
    }

    public bool HasEnemy()
    {
        return this._enemiesStack.Count > 0;
    }
}
