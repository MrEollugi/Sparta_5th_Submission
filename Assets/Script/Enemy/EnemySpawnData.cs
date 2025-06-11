using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enemy Spawn Data
/// Serializable class representing a single enemy spawn configuration.
/// Used in dungeon stages to define which enemies spawn, where, and when.
[System.Serializable]
public class EnemySpawnData
{
    public EnemySO enemySO;
    public Vector3 spawnPosition;
    public float delay;
}
#endregion