using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Dungeon Manager
// Controls dungeon stage flow: starting stage, spawning enemies, and clearing condition check.
public class DungeonManager : MonoBehaviour
{
    #region Singleton
    public static DungeonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    #region Stage Info
    [Tooltip("Currently active dungeon stage.")]
    public DungeonStageSO currentStage;

    private List<GameObject> spawnedEnemies = new();
    #endregion

    #region Dungeon Flow
    // Begins a dungeon by spawning enemies defined in the given stage.
    public void StartDungeon(DungeonStageSO stage)
    {
        currentStage = stage;
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine that spawns each enemy with delay based on enemy spawn data.
    private IEnumerator SpawnEnemies()
    {
        foreach (var data in currentStage.enemyList)
        {
            yield return new WaitForSeconds(data.delay);
            GameObject enemy = Instantiate(data.enemySO.enemyPrefab, data.spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }

        StartCoroutine(CheckClearCondition());
    }

    // Coroutine to continuously check if all enemies are defeated.
    private IEnumerator CheckClearCondition()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            spawnedEnemies.RemoveAll(e => e == null);

            if (spawnedEnemies.Count == 0)
            {
                OnDungeonCleared();
                break;
            }
        }
    }
    #endregion

    #region Dungeon Clear
    // Called when dungeon is cleared; rewards player and shows result UI.
    private void OnDungeonCleared()
    {
        Debug.Log("던전 클리어!");

        // Reward player
        GoldManager.Instance.AddGold(currentStage.goldReward);
        // CrystalManager.Instance.AddCrystal(currentStage.crystalReward);

        foreach (var item in currentStage.rewardItems)
        {
            InventoryManager.Instance.AddItem(item, 1);
        }

        // Show Result
        ResultPanel.Instance.Show(currentStage.goldReward, currentStage.crystalReward, currentStage.rewardItems);
    }
    #endregion
}
#endregion