using UnityEngine;

namespace PunchMan
{
    [CreateAssetMenu(fileName = "Character settings")]
    public class CharacterSettings : ScriptableObject
    {
        [Header("Character move settings")]
        [SerializeField] private float axeZMoveSpeed = 0.02f;
        [SerializeField] private float axeXMoveSpeed = 0.02f;
        [Header("Axe X distance restriction")]
        [SerializeField] private float minDistanceX = -1000;
        [SerializeField] private float maxDistanceX = 1000;

        public float AxeZMoveSpeed => axeZMoveSpeed;
        public float AxeXMoveSpeed => axeXMoveSpeed;
        public float MinDistanceX => minDistanceX;
        public float MaxDistanceX => maxDistanceX;
    }
}