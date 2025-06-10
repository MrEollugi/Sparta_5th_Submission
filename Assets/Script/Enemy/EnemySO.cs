using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;

    [Header("Stats")]
    public int maxHP;
    public int attack;
    public int defense;
    public float moveSpeed;
    public float attackRange;
    public float attackInterval;

    [Header("AI Settings")]
    public float aggroRange;
    public bool isRanged;
}
