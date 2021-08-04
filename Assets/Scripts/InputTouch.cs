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
        
        private float _time;
        
        private void Awake()
        {
            var mainCamera = Camera.main;
            _character = GetComponent<Character>();
            _levelState = GetComponentInParent<LevelState>();
            _characterBehaviour = new CharacterBehaviour(mainCamera, _character, characterSettings, _levelState);
        }
        private void Update()
        {
            if (_levelState.IsGameOver)
                return;
            
            if (_levelState.IsLevelComplete)
                return;
            
            if (_character.IsNearWall())
                return;
            
            if (_levelState.IsBossFight)
            {
                _time += Time.deltaTime;
                if (_time > 5)
                {
                    _levelState.GameOver();
                }
                
                if (Input.GetMouseButtonDown(0))
                    _characterBehaviour.HitBoss();
                
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
                direction = new Vector2(delta.x, 0).normalized;

            _characterBehaviour.LeftRightMove(direction);
            _startPosition = Vector2.zero;
        }

    }
}