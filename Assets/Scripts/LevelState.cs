using System;
using TMPro;
using UnityEngine;

namespace PunchMan
{
    public class LevelState : MonoBehaviour
    {
        private GameState _gameState;
        private EndLevelDisplay _endLevelDisplay; 
        private BossFight _bossFight;

        public bool IsGameOver { get; private set; }
        public bool IsLevelComplete { get; private set; }
        public bool IsBossFight { get; private set; }

        private TMP_Text _bossFightTimerLabel;
        private float _currentTime;

        private void Awake()
        {
            _bossFight = GetComponentInChildren<BossFight>();
            _currentTime = _bossFight.FightTime;
            _bossFightTimerLabel = _bossFight.GetComponentInChildren<TMP_Text>();
            _bossFight.gameObject.SetActive(false);
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
            _bossFight.gameObject.SetActive(false);
            
            _gameState.InteractableNextButton();
        }

        public void BossFight()
        {
            IsBossFight = true;
            _bossFight.gameObject.SetActive(true);
        }

        public float SetTime()
        {
            _currentTime -= Time.deltaTime;
            _bossFightTimerLabel.text = Math.Round(_currentTime, 2).ToString();
            return _currentTime;
        }
    }
}