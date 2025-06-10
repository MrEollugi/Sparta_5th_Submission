using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDungeonType
{
    Normal,
    Gold,
    Event,
    Boss
}


[CreateAssetMenu(menuName = "Stage/DungeonStage")]
public class DungeonStageSO : ScriptableObject
{
    public string stageName;
    public int stageID;
    public EDungeonType dungeonType;

    public List<EnemySpawnData> enemyList;
    public int requiredLevel;
    public int staminaCost;

    public int goldReward;
    public int crystalReward;
    public List<ItemSO> rewardItems;

    public DungeonStageSO nextStage;
}
