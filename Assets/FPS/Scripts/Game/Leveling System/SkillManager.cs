using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that handles the persistance of the player skills, as well as the level and stats of each skill;
/// Regarding functionality, the script utilizes the InstantiateSkills method to spawn three different skills once the shop menu is loaded
/// </summary>
public class SkillManager : MonoBehaviour
{
    // Singleton
    public static SkillManager Instance;

    // List that contains all skills' prefabs
    [SerializeField] private List<GameObject> skillPrefabs = new List<GameObject>();

    // Stats for each skill
    // These will be upgraded and passed on to the instantiated skill prefab each time the shop menu is loaded
    [NonSerialized] public int hpRegenLevel = 1;
    [NonSerialized] public float hpRegenAmount = 5;
    [NonSerialized] public int hpRegenCost = 6;

    [NonSerialized] public int reloadSpeedLevel = 1;
    [NonSerialized] public float reloadSpeedAmount = 0.2f;
    [NonSerialized] public int reloadSpeedCost = 24;

    [NonSerialized] public int criticalDamageLevel = 1;
    [NonSerialized] public float criticalDamageAmount = 10;
    [NonSerialized] public int criticalDamageCost = 4;

    [NonSerialized] public int criticalChanceLevel = 1;
    [NonSerialized] public float criticalChanceAmount = 10;
    [NonSerialized] public int criticalChanceCost = 10;

    [NonSerialized] public int lifeStealLevel = 1;
    [NonSerialized] public float lifeStealAmount = 8;
    [NonSerialized] public int lifeStealCost = 12;

    [NonSerialized] public int coinGainLevel = 1;
    [NonSerialized] public float coinGainAmount = 5;
    [NonSerialized] public int coinGainCost = 15;

    // Player skill stats
    // These will be applied to the player stats
    public float hpRegenPersistent = 0;
    public float reloadSpeedPersistent = 0;
    public float criticalDamagePersistent = 0;
    public float criticalChancePersistent = 0;
    public float lifeStealPersistent = 0;
    public float coinGainPersistent = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Each time the shop menu is loaded, run the method
        if (SceneManager.GetActiveScene().name == "ShopMenuScene")
        {
            InstantiateSkills();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InstantiateSkills()
    {
        List<int> insantiatedSkills = new List<int>() { -1 };
        int spacing = 0;
        int j = -1;

        for (int i = 0; i < 3; i++)
        {
            // Make sure that a skill gets instantiated only once
            while (insantiatedSkills.Contains(j))
            {
                j = UnityEngine.Random.Range(0, skillPrefabs.Count);
            }

            var newSkill = Instantiate(skillPrefabs[j]);
            newSkill.transform.SetParent(GameObject.Find("Canvas").transform);
            newSkill.transform.localPosition = new Vector3(-342 + spacing, -30, 0);
            newSkill.transform.localScale = new Vector3(1, 1, 1);

            // Update spacing
            spacing += 222;

            // Add index of the spawned prefab to the list, so we can skip it
            insantiatedSkills.Add(j);
        }
    }
}
