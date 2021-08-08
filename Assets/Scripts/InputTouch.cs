using UnityEngine;

namespace PunchMan
{
    public class InputTouch : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterBehaviour _characterBehaviour;
        private LevelState _levelState;

        private Vector2 _startPosition;
        
        private void Awake()
        {
            var mainCamera = Camera.main;
            _character = GetComponent<Character>();
            _levelState = GetComponentInParent<LevelState>();
            _characterBehaviour = new CharacterBehaviour(mainCamera, _character, characterSettings, _levelState);
            _character.SetCharacterBehaviour(_characterBehaviour);
        }
        private void Update()
        {
            if (_levelState.IsGameOver)
                return;
            
            if (_levelState.IsLevelComplete)
                return;

            if (_levelState.IsBossFight)
            {
                var time = _levelState.SetTime();
                if (time < 0)
                {
                    _levelState.GameOver();
                }

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _character.HitAudioSource.Play();
                    _characterBehaviour.HitBoss();
                }
                
                return;
            }
            
            _characterBehaviour.ForwardMove();
            
            if (Input.touchCount <= 0)
                return;
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                _startPosition = Input.GetTouch(0).position;

            if (Input.GetTouch(0).phase != TouchPhase.Moved)
                return;
            
            var delta = Input.GetTouch(0).position - _startPosition;
            var direction = Vector2.zero;
                
            if (Mathf.Abs(delta.x) > 0.1f)
                direction = new Vector2(delta.x, 0);

            _characterBehaviour.LeftRightMove(direction);

            _startPosition = Input.GetTouch(0).position;
        }

    }
}