using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHP = 50;
    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP < 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

