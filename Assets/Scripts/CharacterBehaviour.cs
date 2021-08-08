using UnityEngine;

namespace PunchMan
{
    public class CharacterBehaviour
    {
        private readonly Character _character;
        private readonly CharacterSettings _characterSettings;
        private readonly Camera _camera;
        private readonly LevelState _levelState;

        private Boss _boss;
        
        public CharacterBehaviour(Camera camera, Character character, CharacterSettings characterSettings, LevelState levelState)
        {
            _character = character;
            _characterSettings = characterSettings;
            _camera = camera;
            _levelState = levelState;
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

        public void SetCharacterPositionForBossFight()
        {
            var currentCharacterPosition = _character.transform.position;
            var minDistanceX = _characterSettings.MinDistanceX;
            var maxDistanceX = _characterSettings.MaxDistanceX;
            var middleDistanceX = maxDistanceX + minDistanceX;
            var differenceDistanceX = middleDistanceX - currentCharacterPosition.x;

            _character.transform.Translate(differenceDistanceX, 0, 1);
            
            _boss = _character.GetBoss();
        }

        public void HitBoss()
        {
            var characterStrength = _character.Strength;

            _character.RotateCharacterForHit();
            _boss.DecreaseHealth(characterStrength);
            
            if (_boss.Health <= 0)
            {
                _boss.DestroyBoss();
                _levelState.LevelComplete();
            }
        }
    }
}