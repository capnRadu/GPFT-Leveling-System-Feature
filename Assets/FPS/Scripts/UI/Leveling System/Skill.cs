using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Script that handles the value initialization of the skill's updated amounts, and updating of UI text;
/// The script communicates with SkillManager to update the skill amounts to the current ones
/// </summary>
public class Skill : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillLevelText;
    [SerializeField] private TextMeshProUGUI skillAmountText;
    [SerializeField] private TextMeshProUGUI skillCostText;

    [NonSerialized] public int skillLevel;
    [NonSerialized] public float skillAmount;
    [NonSerialized] public int skillCost;

    SkillManager m_SkillManager;

    // Based on what skill this is, updated the corresponding values with the ones from the SkillManager
    private void Awake()
    {
        m_SkillManager = FindObjectOfType<SkillManager>();

        switch (this.gameObject.tag)
        {
            case "Coin Gain Skill":
                skillLevel = m_SkillManager.coinGainLevel;
                skillAmount = m_SkillManager.coinGainAmount;
                skillCost = m_SkillManager.coinGainCost;
                break;
            case "Critical Chance Skill":
                skillLevel = m_SkillManager.criticalChanceLevel;
                skillAmount = m_SkillManager.criticalChanceAmount;
                skillCost = m_SkillManager.criticalChanceCost;
                break;
            case "Critical Damage Skill":
                skillLevel = m_SkillManager.criticalDamageLevel;
                skillAmount = m_SkillManager.criticalDamageAmount;
                skillCost = m_SkillManager.criticalDamageCost;
                break;
            case "HP Regen Skill":
                skillLevel = m_SkillManager.hpRegenLevel;
                skillAmount = m_SkillManager.hpRegenAmount;
                skillCost = m_SkillManager.hpRegenCost;
                break;
            case "Life Steal Skill":
                skillLevel = m_SkillManager.lifeStealLevel;
                skillAmount = m_SkillManager.lifeStealAmount;
                skillCost = m_SkillManager.lifeStealCost;
                break;
            case "Reload Speed Skill":
                skillLevel = m_SkillManager.reloadSpeedLevel;
                skillAmount = m_SkillManager.reloadSpeedAmount;
                skillCost = m_SkillManager.reloadSpeedCost;
                break;
        }
    }

    private void Update()
    {
        skillLevelText.text = $"{skillLevel}";
        skillAmountText.text = $"+{skillAmount}";
        skillCostText.text = $"{skillCost} Coins";
    }
}
