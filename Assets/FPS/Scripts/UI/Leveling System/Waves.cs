using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class Waves : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;

        WaveManager m_WaveManager;

        private void Start()
        {
            m_WaveManager = FindObjectOfType<WaveManager>();
        }

        private void Update()
        {
            waveText.text = $"Wave {m_WaveManager.wave}";
        }
    }
}
