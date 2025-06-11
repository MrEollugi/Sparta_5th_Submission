using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Gold Manager
// Singleton class to manage the player's gold: increase, decrease, and spend operations.
// Broadcasts changes via OnGoldChanged event.
public class GoldManager : MonoBehaviour
{
    #region Singleton
    public static GoldManager Instance { get; private set; }

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

    #region Gold State
    // Current amount of gold the player has.
    public int CurrentGold { get; private set; } = 10000;
    // Event triggered when gold amount changes.
    public event System.Action<int> OnGoldChanged;
    #endregion

    #region Gold Operations
    // Add gold to the player's current amount.
    public void AddGold(int amount)
    {
        CurrentGold += amount;
        OnGoldChanged?.Invoke(CurrentGold);
    }
    // Subtract gold, but never go below 0.
    public void SubtractGold(int amount)
    {
        CurrentGold = Mathf.Max(CurrentGold - amount, 0);
        OnGoldChanged?.Invoke(CurrentGold);
    }
    // Try to spend gold. Returns true if successful.
    public bool SpendGold(int amount)
    {
        if (CurrentGold < amount) return false;

        CurrentGold -= amount;
        OnGoldChanged?.Invoke(CurrentGold);
        return true;
    }
    #endregion
}
#endregion