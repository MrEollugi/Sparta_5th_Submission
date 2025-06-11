using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Dungeon Type Enum
// Enum to define the type of dungeon stage.
public enum EDungeonType
{
    Normal, // Standard dungeon | main story
    Gold,   // Gold farming dungeon
    Event,  // Limited-time or event dungeon
    Boss    // Boss dungeon
}
#endregion


// ScriptableObject representing a dungeon stage's configuration.
[CreateAssetMenu(menuName = "Stage/DungeonStage")]
public class DungeonStageSO : ScriptableObject
{
    #region Basic Info
    [Tooltip("Name of the dungeon stage.")]
    public string stageName;
    
    [Tooltip("Unique ID for the dungeon stage.")]
    public int stageID;
    
    [Tooltip("Type of dungeon.")]
    public EDungeonType dungeonType;
    #endregion

    #region Entry Requirements
    [Tooltip("List of enemies that appear in this stage.")]
    public List<EnemySpawnData> enemyList;
    
    [Tooltip("Required player level to enter.")]
    public int requiredLevel;

    [Tooltip("Stamina cost to enter the stage.")]
    public int staminaCost;
    #endregion

    #region
    [Tooltip("Amount of gold rewarded upon completion.")]
    public int goldReward;

    [Tooltip("Amount of crystal rewarded upon completion.")]
    public int crystalReward;

    [Tooltip("List of item rewards granted upon completion.")]
    public List<ItemSO> rewardItems;
    #endregion

    #region Progression
    [Tooltip("Reference to the next stage in the progression.")]
    public DungeonStageSO nextStage;
    #endregion
}
