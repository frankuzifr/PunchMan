using UnityEngine;

namespace PunchMan
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private int health;

        public int Health => health;

        public void DecreaseHealth(int damage)
        {
            health -= damage;
        }

        public void DestroyBoss()
        {
            Destroy(gameObject);
        }
    }
}