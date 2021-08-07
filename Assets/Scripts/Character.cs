using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace PunchMan
{
    public class Character : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [SerializeField] private int strength;

        public int Strength => strength;

        private CharacterBehaviour _characterBehaviour;
        private CharacterElements _characterElements;
        private Hand _characterHand;
        private LevelState _levelState;
        private Boss _boss;

        private void Awake()
        {
            _levelState = GetComponentInParent<LevelState>();
            _characterElements = GetComponentInChildren<CharacterElements>();
            _characterHand = _characterElements.GetComponentInChildren<Hand>();
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
                if (wallHealth < strength)
                {
                    var handScaleMultiplied = wallHealth / 100f;
                    _characterHand.transform.localScale -= new Vector3(handScaleMultiplied, handScaleMultiplied, handScaleMultiplied);
                    RotateCharacterForHit();
                    wall.DestroyWall();
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
                var handScaleMultiplied = weight.MultipliedStrength / 100f;
                _characterHand.transform.localScale += new Vector3(handScaleMultiplied, handScaleMultiplied, handScaleMultiplied);
                weight.DestroyWeight();
            }

            if (other.TryGetComponent<Burger>(out var burger))
            {
                strength -= burger.MultipliedStrength;
                var handScaleMultiplied = burger.MultipliedStrength / 100f;
                _characterHand.transform.localScale -= new Vector3(handScaleMultiplied, handScaleMultiplied, handScaleMultiplied);
                burger.DestroyBurger();
            }

            if (other.TryGetComponent<Boss>(out var boss))
            {
                _boss = boss;
                _characterBehaviour.SetCharacterPositionForBossFight();
                _levelState.BossFight();
            }
        }

        public void RotateCharacterForHit()
        {
            DOTween.Sequence()
                .Append(_characterElements.transform.DORotate(new Vector3(0, -45, 0), 0.1f))
                .Append(_characterElements.transform.DORotate(new Vector3(0, 0, 0), 0.1f));
        }

        public Boss GetBoss()
            => _boss;
    }
}