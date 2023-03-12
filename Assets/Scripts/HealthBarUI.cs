using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fullHealthBar;

    private void OnEnable() => BoatHealth.updateHealth += SetHealthBar;

    private void OnDisable() => BoatHealth.updateHealth -= SetHealthBar;

    private void SetHealthBar(float amount)
    {
        fullHealthBar.fillAmount = amount;
    }
}