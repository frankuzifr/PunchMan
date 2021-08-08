using UnityEngine;

namespace PunchMan
{
    public class InputMouse : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterBehaviour _characterBehaviour;
        private LevelState _levelState;
        

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

                if (Input.GetMouseButtonDown(0))
                {
                    _character.HitAudioSource.Play();
                    _characterBehaviour.HitBoss();
                }
                
                return;
            }
            
            _characterBehaviour.ForwardMove();
            
            if (!Input.GetMouseButton(0))
                return;

            if ((Input.GetAxis("Mouse X") < 0.001) && (Input.GetAxis("Mouse X") > -0.001)) 
                return;
            
            var direction = new Vector2(Input.GetAxis("Mouse X"), 0);
            _characterBehaviour.LeftRightMove(direction);
        }

    }
}