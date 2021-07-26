﻿using UnityEngine;

namespace PunchMan
{
    public class InputTouch : MonoBehaviour
    {
        [SerializeField] private CharacterSettings characterSettings;
        
        private Character _character;
        private CharacterMovement _characterMovement;

        private Vector2 _startPosition;
        
        private void Awake()
        {
            _character = GetComponent<Character>();
            _characterMovement = new CharacterMovement(_character, characterSettings);
        }
        private void Update()
        {
            _character.transform.Translate(0, 0, characterSettings.AxeXMoveSpeed * Time.deltaTime);
            
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

            _characterMovement.Move(direction);
            _startPosition = Vector2.zero;
        }

    }
}