using System.Collections;
using UnityEngine;

namespace PunchMan
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int health;

        public int Health => health;

        private CharacterBehaviour _characterBehaviour;
        
        private GameState _gameState;
        
        private Boss _boss;

        private bool _isNearWall;

        private void Awake()
        {
            _gameState = GetComponentInParent<GameState>();
        }

        public void SetCharacterBehaviour(CharacterBehaviour characterBehaviour)
        {
            _characterBehaviour = characterBehaviour;
        }

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
                    _gameState.GameOver();
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

            if (other.TryGetComponent<Boss>(out var boss))
            {
                _gameState.BossFight();
                _boss = boss;
                _characterBehaviour.SetCharacterPositionForBossFight();
            }
        }

        public bool IsNearWall()
            => _isNearWall;

        public Boss GetBoss()
            => _boss;

        private IEnumerator DestroyDelay(Wall wall)
        {
            yield return new WaitForSeconds(2f);
            wall.DestroyWall();
            _isNearWall = false;
        }
    }
}