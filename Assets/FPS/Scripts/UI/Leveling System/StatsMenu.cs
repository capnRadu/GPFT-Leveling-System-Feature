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
    [SerializeField] private TextMeshProUGUI hpRegen;
    [SerializeField] private TextMeshProUGUI reloadSpeed;
    [SerializeField] private TextMeshProUGUI criticalDamage;
    [SerializeField] private TextMeshProUGUI criticalChance;
    [SerializeField] private TextMeshProUGUI lifeSteal;
    [SerializeField] private TextMeshProUGUI coinGain;

    WaveManager m_WaveManager;
    SkillManager m_SkillManager;

    private void Start()
    {
        m_WaveManager = FindObjectOfType<WaveManager>();
        m_SkillManager = FindObjectOfType<SkillManager>();
    }

    private void Update()
    {
        currentLevel.text = $"{m_WaveManager.currentLevelPersistent}";
        maxHealth.text = $"{m_WaveManager.maxHealthPersistent}";
        maxSpeed.text = $"{m_WaveManager.maxSpeedPersistent}";
        projectileDamage.text = $"{m_WaveManager.projectileDamagePersistent}";

        hpRegen.text = $"+{m_SkillManager.hpRegenPersistent}";
        reloadSpeed.text = $"+{m_SkillManager.reloadSpeedPersistent}";
        criticalDamage.text = $"+{m_SkillManager.criticalDamagePersistent}";
        criticalChance.text = $"+{m_SkillManager.criticalChancePersistent}%";
        lifeSteal.text = $"+{m_SkillManager.lifeStealPersistent}%";
        coinGain.text = $"+{m_SkillManager.coinGainPersistent}%";
    }
}
