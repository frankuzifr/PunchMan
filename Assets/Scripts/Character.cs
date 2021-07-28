using System;
using System.Collections;
using UnityEngine;

namespace PunchMan
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int health;

        private bool _isNearWall;
        private bool _isGameOver;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("VAR");
            if (other.TryGetComponent<Wall>(out var wall))
            {
                var wallHealth = wall.Health;

                _isNearWall = true;

                if (wallHealth < health)
                {
                    StartCoroutine(DestroyDelay(wall));
                    health -= wallHealth;
                }
                else
                {
                    Debug.Log("Недостаточно силы");
                    _isGameOver = true;
                }
            }

            if (other.TryGetComponent<Weight>(out var weight))
            {
                health++;
                weight.DestroyWeight();
            }

            if (other.TryGetComponent<Burger>(out var burger))
            {
                health--;
                burger.DestroyBurger();
            }
        }

        public bool IsNearWall()
            => _isNearWall;
        
        public bool IsGameOver()
            => _isGameOver;

        private IEnumerator DestroyDelay(Wall wall)
        {
            yield return new WaitForSeconds(2f);
            wall.DestroyWall();
            _isNearWall = false;
        }
    }
}