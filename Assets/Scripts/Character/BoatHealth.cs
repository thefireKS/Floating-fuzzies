using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;
    private int currentHealth;

    public static Action<float> updateHealth;
    public static Action loseGame;

    private void Start()
    {
        currentHealth = fullHealth;
        updateHealth?.Invoke((float)currentHealth / fullHealth);
    }

    private void OnEnable() => EnemyAI.OnDamageDeal += ChangeHealth;
    private void OnDisable() => EnemyAI.OnDamageDeal -= ChangeHealth;

    private void ChangeHealth(int damageDealt)
    {
        currentHealth -= damageDealt;
        updateHealth?.Invoke((float)currentHealth / fullHealth);
        if (currentHealth <= 0)
            loseGame?.Invoke();
    }
}
