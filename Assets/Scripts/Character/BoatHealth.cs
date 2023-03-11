using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = fullHealth;
    }

    private void OnEnable() => EnemyAI.OnDamageDeal += ChangeHealth;
    private void OnDisable() => EnemyAI.OnDamageDeal -= ChangeHealth;

    private void ChangeHealth(int damageDealt)
    {
        currentHealth -= damageDealt;
        Debug.Log(currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Debug.Log("You lose");
    }
}
