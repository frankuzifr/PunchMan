using System;
using TMPro;
using UnityEngine;

namespace PunchMan
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private int health;

        public int Health => health;

        private TMP_Text _healthInfoLabel;

        private void Awake()
        {
            _healthInfoLabel = GetComponentInChildren<TMP_Text>();
            _healthInfoLabel.text = health.ToString();
        }

        public void DecreaseHealth(int damage)
        {
            health -= damage;
            _healthInfoLabel.text = health.ToString();
        }

        public void DestroyBoss()
        {
            Destroy(gameObject);
        }
    }
}