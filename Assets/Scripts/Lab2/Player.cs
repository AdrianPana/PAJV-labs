using System;
using UnityEngine;

namespace Lab2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerEventSO playerEventSO;
        public int health = 100;
        private int _maxHealth = 100;

        private void Start()
        {
            _maxHealth = health = LevelManager.Instance.maxHp;
            UpdatePlayerHealth(health, _maxHealth);
        }

        public void Heal(int amount)
        {
            health += amount;
            if (health > _maxHealth)
                health = _maxHealth;
            UpdatePlayerHealth(health, _maxHealth);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health < 0)
                health = 0;
            UpdatePlayerHealth(health, _maxHealth);
        }

        public void UpdatePlayerHealth(int health, int maxHealth)
        {
            playerEventSO.NotifyPlayerHealthChanged(health, maxHealth);
        }

        public void AddXP(int amount)
        {
            LevelManager.Instance.AddXP(amount);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("XP"))
            {
                AddXP(1);
                Destroy(other.gameObject);
            }
        }
    }
}