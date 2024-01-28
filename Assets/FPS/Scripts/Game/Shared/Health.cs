using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] public float MaxHealth = 10f;

        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickup() => CurrentHealth < MaxHealth;

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        // LEVELING SYSTEM
        public PlayerResources PlayerResources { get; private set; }
        private GameObject player;

        public int coinsReward;
        public float XpReward;

        private WaveManager WaveManager;
        private SkillManager SkillManager;

        private float hpRegenAmount;
        private bool isRegenHealth = false;
        private float combatTimer = 0f;

        private float criticalDamageAmount = 0;
        private float criticalChanceAmount = 0;

        private float lifeStealAmount = 0;
        //

        private void Awake()
        {
            // LEVELING SYSTEM
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerResources = player.GetComponent<PlayerResources>();
            WaveManager = FindObjectOfType<WaveManager>();
            SkillManager = FindObjectOfType<SkillManager>();

            // Set the player health value to the one from the wave manager so the health upgrades are applied, and the HP regen amount to the one from the skill manager
            if (this.gameObject == player)
            {
                MaxHealth = WaveManager.maxHealthPersistent;
                hpRegenAmount = SkillManager.hpRegenPersistent;
            }
            // Because the critical system functions in the Health script, we set the critical damage and chance values to the enemy instead
            // Same with the life steal system
            else
            {
                criticalDamageAmount = SkillManager.criticalDamagePersistent;
                criticalChanceAmount = SkillManager.criticalChancePersistent;
                lifeStealAmount = SkillManager.lifeStealPersistent;
            }
            //
        }

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            // LEVELING SYSTEM
            // Deal with the HP regeneration mechanic if the skill is unlocked
            if (hpRegenAmount != 0)
            {
                if (combatTimer < 1.5f)
                {
                    // Value that stores the number of seconds the player is not hit by an enemy
                    combatTimer += Time.deltaTime;
                }

                // Activate HP regeneration only if the player is not continuously in combat
                if (CurrentHealth != MaxHealth && !isRegenHealth && combatTimer >= 1.5f)
                {
                    StartCoroutine(RegenHP());
                }
            }
            // 
        }

        // LEVELING SYSTEM
        private IEnumerator RegenHP()
        {
            isRegenHealth = true;

            while (CurrentHealth < MaxHealth && CurrentHealth > 0f && combatTimer >= 1.5f)
            {
                Heal(hpRegenAmount);
                yield return new WaitForSeconds(1);
            }

            isRegenHealth = false;
        }
        //

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            float healthBefore = CurrentHealth;

            // LEVELING SYSTEM
            // Critical chance / damage system
            // Given the fact that criticalChanceAmount is set to 0 for the player, they will only receive the standard damage from the enemy
            float randomValue = UnityEngine.Random.Range(0f, 100f);
                                
            if (randomValue < criticalChanceAmount)
            {
                CurrentHealth -= criticalDamageAmount;
            }
            else
            {
                CurrentHealth -= damage;
            }

            // Life steal system
            // Apply life steal amount to the player's HP only if the enemy is killed
            if (this.CurrentHealth <= 0f)
            {
                damageSource.GetComponent<Health>().CurrentHealth += lifeStealAmount / 100 * this.MaxHealth;
                damageSource.GetComponent<Health>().CurrentHealth = Mathf.Clamp(damageSource.GetComponent<Health>().CurrentHealth, 0f, damageSource.GetComponent<Health>().MaxHealth);
            }
            //

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // LEVELING SYSTEM
            // When the player is hit, reset the combat timer back to 0
            if (hpRegenAmount != 0)
            {
                combatTimer = 0f;
            }
            //

            // call OnDamage action
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            // call OnDie action
            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();

                // LEVELING SYSTEM
                // Call methods only if the enemy is killed
                if (this.gameObject != player)
                {
                    PlayerResources.GainCoins(coinsReward);
                    PlayerResources.GainXP(XpReward);
                }
                //
            }
        }
    }
}