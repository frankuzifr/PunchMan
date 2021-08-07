using System;
using UnityEngine;

namespace PunchMan
{
    public class LevelState : MonoBehaviour
    {
        private GameState _gameState;
        private EndLevelDisplay _endLevelDisplay; 
        
        public bool IsGameOver { get; private set; }
        public bool IsLevelComplete { get; private set; }
        public bool IsBossFight { get; private set; }

        private void Awake()
        {
            _gameState = GetComponentInParent<GameState>();
            _endLevelDisplay = _gameState.EndLevelDisplay;
        }

        public void GameOver()
        {
            IsGameOver = true;
            _endLevelDisplay.gameObject.SetActive(true);
            _endLevelDisplay.NextLevel.gameObject.SetActive(false);
            _endLevelDisplay.LevelState.text = "GameOver";
            _endLevelDisplay.LevelState.color = Color.red;
        } 
        
        public void LevelComplete()
        {
            IsLevelComplete = true;
            _endLevelDisplay.gameObject.SetActive(true);
            _endLevelDisplay.LevelState.text = "Level complete!";
            _endLevelDisplay.LevelState.color = Color.green;
            _endLevelDisplay.NextLevel.gameObject.SetActive(!_gameState.IsLastLevel());
            
            _gameState.InteractableNextButton();
        }

        public void BossFight()
        {
            IsBossFight = true;
        }
    }
}