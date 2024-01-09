using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Game
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;

        [SerializeField] private GameObject enemyBot;
        private GameFlowManager flowManager;

        public int wave = 0;
        private int baseEnemiesPerWave = 4;
        private int enemiesIncreasePerWave = 1;

        private int enemyHealthIncrease = -2;
        private int enemyCoinRewardIncrease = -4;
        private float enemyXpRewardIncrease = -5;

        public bool patrolPathSignal = false;

        // Player Resources
        public int coinsPersistent = 0;
        public float currentXpPersistent = 0;
        public float levelUpXpPersistent = 100;
        public float levelUpXpMultiplierPersistent = 0.4f;
        public int currentLevelPersistent = 0;
        public int levelUpAmountPersistent = 0;

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
            NextWave();
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

            int enemiesToSpawn = baseEnemiesPerWave + (enemiesIncreasePerWave * (wave - 1));
            patrolPathSignal = true;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(-2.4f, 3), enemyPrefab.transform.position.y, Random.Range(-4.3f, 1.2f));
                Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                var enemy = Instantiate(enemyPrefab, randomSpawnPosition, spawnRotation);

                enemy.GetComponentInChildren<Health>().MaxHealth += enemyHealthIncrease;
                enemy.GetComponentInChildren<Health>().coinsReward += enemyCoinRewardIncrease;
                enemy.GetComponentInChildren<Health>().XpReward += enemyXpRewardIncrease;
            }
        }
    }
}
