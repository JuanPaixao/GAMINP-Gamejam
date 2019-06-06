using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBackup : MonoBehaviour
{
    private Stack<GameObject> _enemiesStack;
    [SerializeField] private int _quantity;
    [SerializeField] private GameObject[] _enemies;

    void Start()
    {
        this._enemiesStack = new Stack<GameObject>();
        this.CreateAllEnemies();
    }

    internal void ReturnEnemy(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    void Update()
    {

    }
    private void CreateAllEnemies()
    {
        for (int i = 0; i < this._quantity; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, 2);
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
        Debug.Log("Stacked " + enemy);
    }

    public bool HasEnemy()
    {
        return this._enemiesStack.Count > 0;
    }
}
