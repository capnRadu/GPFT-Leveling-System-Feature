using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class ChooseUpgrade : MonoBehaviour
{
    WaveManager m_WaveManager;

    private int healthUpgradeAmount = 3;
    private int speedUpgradeAmount = 2;
    private int damageUpgradeAmount = 1;

    private void Start()
    {
        m_WaveManager = FindObjectOfType<WaveManager>();
    }

    public void Upgrade(string upgradeName)
    {
        if (m_WaveManager.levelUpAmountPersistent > 0)
        {
            switch (upgradeName)
            {
                case "health":
                    m_WaveManager.maxHealthPersistent += healthUpgradeAmount;
                    break;
                case "speed":
                    m_WaveManager.maxSpeedPersistent += speedUpgradeAmount;
                    break;
                case "damage":
                    m_WaveManager.projectileDamagePersistent += damageUpgradeAmount;
                    break;
            }

            m_WaveManager.levelUpAmountPersistent -= 1;
        }
    }
}
