using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finishedWave;
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
        finishedWave.text = $"Shop (Wave {m_WaveManager.wave})";
        coinsAmount.text = $"{m_WaveManager.coinsPersistent}";
    }
}
