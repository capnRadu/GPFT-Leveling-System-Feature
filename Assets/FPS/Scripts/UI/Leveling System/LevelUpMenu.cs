using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finishedWave;
    [SerializeField] private TextMeshProUGUI levelUpAmount;
    [SerializeField] private TextMeshProUGUI coinsAmount;

    WaveManager m_WaveManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        m_WaveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        finishedWave.text = $"Wave {m_WaveManager.wave} Complete";
        levelUpAmount.text = $"+{m_WaveManager.levelUpAmountPersistent} Upgrade Points";
        coinsAmount.text = $"{m_WaveManager.coinsPersistent}";
    }
}
