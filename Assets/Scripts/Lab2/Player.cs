using System;
using UnityEngine;

namespace Lab2
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerEventSO playerEventSO;
        public int health = 100;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                TakeDamage(1);
            }
        }
    }
}