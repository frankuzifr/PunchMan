using UnityEngine;

namespace PunchMan
{
    public class Wall : MonoBehaviour
    { 
        [SerializeField] private int health;

        public int Health => health;

        public void DestroyWall()
        {
            Destroy(gameObject);
        }
    }
}