using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class PlayerResources : MonoBehaviour
    {
        private WaveManager WaveManager;

        public int coins;

        public float currentXp;
        public float levelUpXp;
        private float levelUpXpMultiplier;

        public int currentLevel;
        public int levelUpAmount;

        private void Awake()
        {
            WaveManager = FindObjectOfType<WaveManager>();

            coins = WaveManager.coinsPersistent;
            currentXp = WaveManager.currentXpPersistent;
            levelUpXp = WaveManager.levelUpXpPersistent;
            levelUpXpMultiplier = WaveManager.levelUpXpMultiplierPersistent;
            currentLevel = WaveManager.currentLevelPersistent;
            levelUpAmount = WaveManager.levelUpAmountPersistent;
        }

        private void OnDisable()
        {
            WaveManager.coinsPersistent = coins;
            WaveManager.currentXpPersistent = currentXp;
            WaveManager.levelUpXpPersistent = levelUpXp;
            WaveManager.levelUpXpMultiplierPersistent = levelUpXpMultiplier;
            WaveManager.currentLevelPersistent = currentLevel;
            WaveManager.levelUpAmountPersistent = levelUpAmount;
        }

        public void GainCoins(int amount)
        {
            coins += amount;
        }

        public void GainXP(float amount)
        {
            currentXp += amount;

            HandleXP();
        }

        private void HandleXP()
        {
            if (currentXp >= levelUpXp)
            {
                currentXp = 0;
                levelUpXp += levelUpXp * levelUpXpMultiplier;
                currentLevel++;
                levelUpAmount++;
            }
        }
    }
}
