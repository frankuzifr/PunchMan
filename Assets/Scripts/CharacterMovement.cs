using UnityEngine;

namespace PunchMan
{
    public class CharacterMovement
    {
        private readonly Character _character;
        private readonly CharacterSettings _characterSettings;
        
        public CharacterMovement(Character character, CharacterSettings characterSettings)
        {
            _character = character;
            _characterSettings = characterSettings;
        }
        
        public void Move(Vector2 direction)
        {
            var currentPosition = _character.transform.position;
            var totalSpeed = _characterSettings.AxeXMoveSpeed * Time.deltaTime;
            var endPosition = currentPosition + (Vector3)direction * totalSpeed;

            endPosition.x = Mathf.Clamp(endPosition.x, _characterSettings.MinDistanceX, _characterSettings.MaxDistanceX);

            _character.transform.position = endPosition;
        }
    }
}