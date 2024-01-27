using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

/// <summary>
/// Script that handles the purchasing of the skills, and their stats' upgrade once they are bought;
/// The script communicates with SkillManager to update the stats values of a skill, and the skill amount value that will be applied to the player stats;
/// The script communicates with WaveManager to check and update the player's coins amount
/// </summary>
public class BuySkill : MonoBehaviour
{
    SkillManager m_SkillManager;
    WaveManager m_WaveManager;

    private bool hasBought = false;

    private void Start()
    {
        m_SkillManager = FindObjectOfType<SkillManager>();
        m_WaveManager = FindObjectOfType<WaveManager>();
    }

    public void PurchaseSkill(string skillName)
    {
        switch (skillName)
        {
            case "HP Regen":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.hpRegenCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.hpRegenPersistent = m_SkillManager.hpRegenAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.hpRegenCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.hpRegenLevel += 1;
                    m_SkillManager.hpRegenAmount += 5;
                    m_SkillManager.hpRegenCost += 20;
                }
                break;
            case "Reload Speed":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.reloadSpeedCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.reloadSpeedPersistent = m_SkillManager.reloadSpeedAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.reloadSpeedCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.reloadSpeedLevel += 1;
                    m_SkillManager.reloadSpeedAmount += 10f;
                    m_SkillManager.reloadSpeedCost += 40;
                }
                break;
            case "Critical Damage":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.criticalDamageCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.criticalDamagePersistent = m_SkillManager.criticalDamageAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.criticalDamageCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.criticalDamageLevel += 1;
                    m_SkillManager.criticalDamageAmount += 10;
                    m_SkillManager.criticalDamageCost += 15;
                }
                break;
            case "Critical Chance":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.criticalChanceCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.criticalChancePersistent = m_SkillManager.criticalChanceAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.criticalChanceCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.criticalChanceLevel += 1;
                    m_SkillManager.criticalChanceAmount += 6;
                    m_SkillManager.criticalChanceCost += 30;
                }
                break;
            case "Life Steal":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.lifeStealCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.lifeStealPersistent = m_SkillManager.lifeStealAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.lifeStealCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.lifeStealLevel += 1;
                    m_SkillManager.lifeStealAmount += 10;
                    m_SkillManager.lifeStealCost += 25;
                }
                break;
            case "Coin Gain":
                if (m_WaveManager.coinsPersistent >= m_SkillManager.coinGainCost)
                {
                    // Update corresponding persistent value that will be applied to the player
                    m_SkillManager.coinGainPersistent = m_SkillManager.coinGainAmount;

                    // Update player coins amount
                    m_WaveManager.coinsPersistent -= m_SkillManager.coinGainCost;
                    hasBought = true;

                    // Increase skill level and stats
                    m_SkillManager.coinGainLevel += 1;
                    m_SkillManager.coinGainAmount += 10;
                    m_SkillManager.coinGainCost += 35;
                }
                break;
        }

        // Destroy the prefab only if the player bought the corresponding skill
        if (hasBought)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
