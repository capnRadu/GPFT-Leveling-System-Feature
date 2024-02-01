using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Game
{
    /// <summary>
    /// Script that handles the persistance of the player upgrades and resources, as well as the functionality of the wave system;
    /// Regarding functionality, the script utilizes the NextWave method to spawn enemies, and increase their difficulty and rewards
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        // Singleton
        public static WaveManager Instance;

        // Prefab for the enemy (the prefab contains the patrol path gameobject, with the enemy bot being its child, so each enemy has its unique patrol path;
        // see Assets/FPS/Prefabs/Leveling System)
        [SerializeField] private GameObject enemyBot;

        private GameFlowManager flowManager;

        public int wave = 0;
        public float waveTimer = 0f;
        private bool hasSpawed = false;
        private bool isFinished = false;
        
        // Increase the number of enemies after each wave
        private int baseEnemiesPerWave = 5;
        private int enemiesIncreasePerWave = 2;

        // Increase the enemies' health, and rewards after each wave
        private int enemyHealthIncrease = -5;
        private int enemyCoinRewardIncrease = -2;
        private float enemyXpRewardIncrease = -3;

        // Player resources
        public float coinsPersistent = 0;
        public float currentXpPersistent = 0;
        public float levelUpXpPersistent = 100;
        public float levelUpXpMultiplierPersistent = 1.2f;
        public int currentLevelPersistent = 0;
        public int levelUpAmountPersistent = 0;

        // Player upgrade stats
        public float maxHealthPersistent = 100f;
        public float projectileDamagePersistent = 15f;
        public float maxSpeedPersistent = 7f;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            flowManager = FindObjectOfType<GameFlowManager>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Each time the main scene is loaded, run the method
            if (SceneManager.GetActiveScene().name != "LevelUpMenuScene" && SceneManager.GetActiveScene().name != "ShopMenuScene")
            {
                NextWave();
            }
        }

        private void Update()
        {
            // Keep spawning enemies until the wave timer reaches zero only if all previous enemies are eliminated
            if (waveTimer >= 0)
            {
                waveTimer -= Time.deltaTime;

                if (GameObject.FindGameObjectsWithTag("Patrol").Length == 0 && !hasSpawed)
                {
                    StartCoroutine(SpawnEnemies(enemyBot));
                }
            }
            else if (waveTimer < 0)
            {
                if (GameObject.FindGameObjectsWithTag("Patrol").Length == 0 && !isFinished)
                {
                    // Signal that all enemies are eliminated and the wave timer has ran out
                    EventManager.Broadcast(Events.AllObjectivesCompletedEvent);
                    isFinished = true;
                }
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void NextWave()
        {
            if (flowManager)
            {
                FindObjectOfType<GameFlowManager>().PlaySound();
            }

            wave++;
            waveTimer = 45f;
            hasSpawed = false;
            isFinished = false;

            enemyHealthIncrease += 5;
            enemyCoinRewardIncrease += 2;
            enemyXpRewardIncrease += 3;
        }

        private IEnumerator SpawnEnemies(GameObject enemyPrefab)
        {
            hasSpawed = true;

            yield return new WaitForSeconds(2f);

            // This check prevents enemy spawning in case the event has been broadcasted but the coroutine already started
            if (!isFinished)
            {
                // Increase the number of enemies and choose a random value that is bigger than the half
                int increaseEnemies = baseEnemiesPerWave + (enemiesIncreasePerWave * (wave - 1));
                int enemiesToSpawn = Random.Range((int) increaseEnemies / 2, increaseEnemies + 1);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    var x = Random.Range(1, 10000);
                    Random.InitState(x);

                    Vector3 randomSpawnPosition = new Vector3(Random.Range(0f, 26f), enemyPrefab.transform.position.y, Random.Range(-16f, 13f));
                    Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                    var enemy = Instantiate(enemyPrefab, randomSpawnPosition, spawnRotation);

                    // Increase the enemy's stats
                    enemy.GetComponentInChildren<Health>().MaxHealth += enemyHealthIncrease;
                    enemy.GetComponentInChildren<Health>().coinsReward += enemyCoinRewardIncrease;
                    enemy.GetComponentInChildren<Health>().XpReward += enemyXpRewardIncrease;

                    // Signal that the spawn has completed
                    hasSpawed = false;
                }
            }
        }

        // Reset wave values back to standard ones
        public void ResetWave()
        {
            wave = 0;
            enemyHealthIncrease = -5;
            enemyCoinRewardIncrease = -2;
            enemyXpRewardIncrease = -3;
            coinsPersistent = 0;
    }
    }
}
