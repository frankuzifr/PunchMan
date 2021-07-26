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
            _character = GetComponent<Character>();
            _characterMovement = new CharacterMovement(_character, characterSettings);
        }

        private void Update()
        {
            _character.transform.Translate(0, 0, characterSettings.AxeZMoveSpeed * Time.deltaTime);
            
            if (!Input.GetMouseButton(0))
                return;

            if ((Input.GetAxis("Mouse X") < 0.1) && (Input.GetAxis("Mouse X") > -0.1)) 
                return;
            
            var direction = new Vector2(Input.GetAxis("Mouse X"), 0).normalized;
            _characterMovement.Move(direction);
        }

    }
}