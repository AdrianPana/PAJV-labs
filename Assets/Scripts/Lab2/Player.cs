using System;
using UnityEngine;

namespace Lab2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerEventSO playerEventSO;
        public int health = 100;
        public int xp = 0;

        private void Start()
        {
            UpdatePlayerHealth(health);
        }

        public void Heal(int amount)
        {
            health += amount;
            UpdatePlayerHealth(health);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            UpdatePlayerHealth(health);
        }

        public void UpdatePlayerHealth(int health)
        {
            playerEventSO.NotifyPlayerHealthChanged(health);
        }

        public void AddXP(int amount)
        {
            xp += amount;
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