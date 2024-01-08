using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class PlayerResources : MonoBehaviour
    {
        public int coins = 0;

        public float currentXp = 0;
        public float levelUpXp = 100;
        public int currentLevel = 0;
        private float levelUpXpMultiplier = 0.4f;

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
            }
        }
    }
}
