using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI maxSpeed;
    [SerializeField] private TextMeshProUGUI projectileDamage;

    WaveManager m_WaveManager;

    private void Start()
    {
        m_WaveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        currentLevel.text = $"{m_WaveManager.currentLevelPersistent}";
        maxHealth.text = $"{m_WaveManager.maxHealthPersistent}";
        maxSpeed.text = $"{m_WaveManager.maxSpeedPersistent}";
        projectileDamage.text = $"{m_WaveManager.projectileDamagePersistent}";
    }
}
