using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enemy ScriptableObject
/// ScriptableObject containing configuration data for an enemy.
/// Used by spawner systems or enemy managers to instantiate and initialize enemies.
[CreateAssetMenu(menuName = "Enemy/Enemy")]
public class EnemySO : ScriptableObject
{
    #region Basic Info
    [Tooltip("Name of the enemy.")]
    public string enemyName;

    [Tooltip("Prefab to instantiate for this enemy.")]
    public GameObject enemyPrefab;
    #endregion

    #region Stats
    [Header("Stats")]
    [Tooltip("Maximum HP of the enemy.")]
    public int maxHP;

    [Tooltip("Attack value of the enemy.")]
    public int attack;

    [Tooltip("Defense value of the enemy.")]
    public int defense;

    [Tooltip("Movement speed of the enemy.")]
    public float moveSpeed;

    [Tooltip("Distance the enemy can reach with an attack.")]
    public float attackRange;

    [Tooltip("Interval between each attack (in seconds).")]
    public float attackInterval;
    #endregion

    #region AI Behavior
    [Header("AI Settings")]
    [Tooltip("Detection range to engage the player.")]
    public float aggroRange;

    [Tooltip("Whether the enemy attacks from range.")]
    public bool isRanged;
    #endregion
}
#endregion