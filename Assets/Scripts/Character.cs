using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace PunchMan
{
    public class Character : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [SerializeField] private int strength;

        public int Strength => strength;

        private CharacterBehaviour _characterBehaviour;
        
        private LevelState _levelState;
        
        private Boss _boss;

        private bool _isNearWall;

        private void Awake()
        {
            _levelState = GetComponentInParent<LevelState>();
        }

        public void SetCharacterBehaviour(CharacterBehaviour characterBehaviour)
        {
            _characterBehaviour = characterBehaviour;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Wall>(out var wall))
            {
                var wallHealth = wall.Health;

                _isNearWall = true;

                if (wallHealth < strength)
                {
                    StartCoroutine(DestroyDelay(wall));
                    strength -= wallHealth;
                }
                else
                {
                    Debug.Log("Недостаточно силы");
                    _levelState.GameOver();
                }
            }

            if (other.TryGetComponent<Weight>(out var weight))
            {
                strength += weight.MultipliedStrength;
                weight.DestroyWeight();
            }

            if (other.TryGetComponent<Burger>(out var burger))
            {
                strength -= burger.MultipliedStrength;
                burger.DestroyBurger();
            }

            if (other.TryGetComponent<Boss>(out var boss))
            {
                _boss = boss;
                _characterBehaviour.SetCharacterPositionForBossFight();
                _levelState.BossFight();
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