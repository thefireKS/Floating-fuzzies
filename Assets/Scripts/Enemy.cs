using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHitPoints;
    private float _currentHitPoints;

    private void OnEnable()
    {
        _currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(float damage)
    {
        _currentHitPoints -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(_currentHitPoints <= 0) Destroy(gameObject);
    }
}
