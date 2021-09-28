using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField] 
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag)
    {
        // buat game object enemy sesuai tag
        GameObject enemy = Instantiate(enemyPrefab[tag], transform);
        return enemy;
    }
}
