using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyScriptable", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    public GameObject enemyPrefab;
    public int spawnRarity;
}
