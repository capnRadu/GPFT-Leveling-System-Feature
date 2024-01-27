﻿using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(SceneName);
        }

        // LEVELING SYSTEM
        // Load scene for the level up menu; load next scene only if the player has used all upgrade points
        public void LoadTargetSceneShopMenu()
        {
            if (FindObjectOfType<WaveManager>().levelUpAmountPersistent == 0)
            {
                SceneManager.LoadScene(SceneName);
            }
        }

        // Load next wave for the shop menu
        public void LoadTargetSceneNextWave()
        {
            SceneManager.LoadScene(SceneName);
        }

        // Destroy the player upgrades progress when a new run / menu is loaded
        public void NewRunOrMenu()
        {
            Destroy(FindObjectOfType<WaveManager>().gameObject);
            SceneManager.LoadScene(SceneName);
        }

        // Keep the player upgrades progress on respawn
        public void Respawn()
        {
            FindObjectOfType<WaveManager>().ResetWave();
            SceneManager.LoadScene(SceneName);
        }
        //
    }
}