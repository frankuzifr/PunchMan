using UnityEngine;

namespace PunchMan
{
    public class InputMouse : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterBehaviour _characterBehaviour;
        private GameState _gameState;
        
        private void Awake()
        {
            var mainCamera = Camera.main;
            _character = GetComponent<Character>();
            _gameState = GetComponentInParent<GameState>();
            _characterBehaviour = new CharacterBehaviour(mainCamera, _character, characterSettings, _gameState);
            _character.SetCharacterBehaviour(_characterBehaviour);
        }

        private void Update()
        {
            if (_gameState.IsGameOver)
                return;
            
            if (_character.IsNearWall())
                return;
            
            if (_gameState.IsBossFight)
            {
                if (Input.GetMouseButtonDown(0))
                    _characterBehaviour.HitBoss();
                
                return;
            }
            
            _characterBehaviour.ForwardMove();
            
            if (!Input.GetMouseButton(0))
                return;

            if ((Input.GetAxis("Mouse X") < 0.001) && (Input.GetAxis("Mouse X") > -0.001)) 
                return;
            
            var direction = new Vector2(Input.GetAxis("Mouse X"), 0).normalized;
            _characterBehaviour.LeftRightMove(direction);
        }

    }
}