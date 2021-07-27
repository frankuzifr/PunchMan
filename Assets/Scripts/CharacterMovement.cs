using UnityEngine;

namespace PunchMan
{
    public class CharacterMovement
    {
        private readonly Character _character;
        private readonly CharacterSettings _characterSettings;
        private readonly Camera _camera;
        
        public CharacterMovement(Camera camera, Character character, CharacterSettings characterSettings)
        {
            _character = character;
            _characterSettings = characterSettings;
            _camera = camera;
        }

        public void ForwardMove()
        {
             _character.transform.Translate(0, 0, _characterSettings.AxeZMoveSpeed * Time.deltaTime, Space.Self);
             _camera.transform.Translate(0, 0, _characterSettings.AxeZMoveSpeed * Time.deltaTime, Space.World);
        }
        
        public void LeftRightMove(Vector2 direction)
        {
            var currentPosition = _character.transform.position;
            var totalSpeed = _characterSettings.AxeXMoveSpeed * Time.deltaTime;
            var endPosition = currentPosition + (Vector3)direction * totalSpeed;

            endPosition.x = Mathf.Clamp(endPosition.x, _characterSettings.MinDistanceX, _characterSettings.MaxDistanceX);

            _character.transform.position = endPosition;
        }
    }
}