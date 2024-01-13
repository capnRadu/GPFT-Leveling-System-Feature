using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Game
{
    public class WaveManager : MonoBehaviour
    {
        // Singleton
        public static WaveManager Instance;

        // Prefab for the enemy (the prefab contains the patrol path gameobject, with the enemy bot being its child, so each enemy has its unique patrol path;
        // see Assets/FPS/Prefabs/Leveling System)
        [SerializeField] private GameObject enemyBot;

        private GameFlowManager flowManager;

        public int wave = 0;
        
        // Increase the number of enemies after each wave
        private int baseEnemiesPerWave = 4;
        private int enemiesIncreasePerWave = 1;

        // Increase the enemies' health, and rewards after each wave
        private int enemyHealthIncrease = -2;
        private int enemyCoinRewardIncrease = -4;
        private float enemyXpRewardIncrease = -5;

        // Player resources
        public int coinsPersistent = 0;
        public float currentXpPersistent = 0;
        public float levelUpXpPersistent = 100;
        public float levelUpXpMultiplierPersistent = 0.4f;
        public int currentLevelPersistent = 0;
        public int levelUpAmountPersistent = 0;

        // Player upgradable stats
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
            // Each time a new scene is loaded, and if the player hasn't leveled up, run the method
            if (SceneManager.GetActiveScene().name != "LevelUpMenuScene")
            {
                NextWave();
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
            enemyHealthIncrease += 2;
            enemyCoinRewardIncrease += 4;
            enemyXpRewardIncrease += 5;
            StartCoroutine(SpawnEnemies(enemyBot));
        }

        private IEnumerator SpawnEnemies(GameObject enemyPrefab)
        {
            yield return new WaitForSeconds(2f);

            // Increase the number of enemies
            int enemiesToSpawn = baseEnemiesPerWave + (enemiesIncreasePerWave * (wave - 1));

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var x = Random.Range(1, 10000);
                Random.InitState(x);

                Vector3 randomSpawnPosition = new Vector3(Random.Range(-2.4f, 3), enemyPrefab.transform.position.y, Random.Range(-4.3f, 1.2f));
                Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                var enemy = Instantiate(enemyPrefab, randomSpawnPosition, spawnRotation);

                // Increase the enemy's stats
                enemy.GetComponentInChildren<Health>().MaxHealth += enemyHealthIncrease;
                enemy.GetComponentInChildren<Health>().coinsReward += enemyCoinRewardIncrease;
                enemy.GetComponentInChildren<Health>().XpReward += enemyXpRewardIncrease;
            }
        }
    }
}
