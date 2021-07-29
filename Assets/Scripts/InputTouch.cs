using UnityEngine;

namespace PunchMan
{
    public class InputTouch : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterBehaviour _characterBehaviour;
        private GameState _gameState;

        private Vector2 _startPosition;
        
        private void Awake()
        {
            var mainCamera = Camera.main;
            _character = GetComponent<Character>();
            _gameState = GetComponentInParent<GameState>();
            _characterBehaviour = new CharacterBehaviour(mainCamera, _character, characterSettings, _gameState);
        }
        private void Update()
        {
            if (_gameState.IsGameOver)
                return;
            
            if (_character.IsNearWall())
                return;
            
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
                direction = new Vector2(delta.x, 0).normalized;

            _characterBehaviour.LeftRightMove(direction);
            _startPosition = Vector2.zero;
        }

    }
}