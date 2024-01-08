using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class Resources : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsAmountText;

        [SerializeField] private Image XpFillImage;
        [SerializeField] private TextMeshProUGUI currentLevelText;

        PlayerResources m_PlayerResources;

        private void Start()
        {
            PlayerCharacterController playerCharacterController = GameObject.FindObjectOfType<PlayerCharacterController>();
            m_PlayerResources = playerCharacterController.GetComponent<PlayerResources>();
        }

        private void Update()
        {
            coinsAmountText.text = $"{m_PlayerResources.coins}";

            XpFillImage.fillAmount = m_PlayerResources.currentXp / m_PlayerResources.levelUpXp;
            currentLevelText.text = $"{m_PlayerResources.currentLevel}";
        }
    }
}
