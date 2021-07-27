using UnityEngine;

namespace PunchMan
{
    public class InputMouse : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterMovement _characterMovement;
        
        private void Awake()
        {
            var mainCamera = Camera.main;
            _character = GetComponent<Character>();
            _characterMovement = new CharacterMovement(mainCamera, _character, characterSettings);
        }

        private void Update()
        {
            if (_character.IsGameOver())
                return;
            
            if (_character.IsNearWall())
                return;
            
            _characterMovement.ForwardMove();
            
            if (!Input.GetMouseButton(0))
                return;

            if ((Input.GetAxis("Mouse X") < 0.1) && (Input.GetAxis("Mouse X") > -0.1)) 
                return;
            
            var direction = new Vector2(Input.GetAxis("Mouse X"), 0).normalized;
            _characterMovement.LeftRightMove(direction);
        }

    }
}