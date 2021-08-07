using System;
using UnityEngine;

namespace PunchMan
{
    public class LevelState : MonoBehaviour
    {
        private LevelsContainer _levelsContainer;
        private EndLevelDisplay _endLevelDisplay; 
        
        public bool IsGameOver { get; private set; }
        public bool IsLevelComplete { get; private set; }
        public bool IsBossFight { get; private set; }

        private void Awake()
        {
            _levelsContainer = GetComponentInParent<LevelsContainer>();
            _endLevelDisplay = _levelsContainer.EndLevelDisplay;
        }

        public void GameOver()
        {
            IsGameOver = true;
            _endLevelDisplay.gameObject.SetActive(true);
            _endLevelDisplay.NextLevel.gameObject.SetActive(false);
            _endLevelDisplay.LevelState.text = "GameOver";
            _endLevelDisplay.LevelState.color = Color.red;

            Debug.Log("Level failed!");
        } 
        
        public void LevelComplete()
        {
            IsLevelComplete = true;
            _endLevelDisplay.gameObject.SetActive(true);
            _endLevelDisplay.NextLevel.gameObject.SetActive(true);
            _endLevelDisplay.LevelState.text = "Level complete!";
            _endLevelDisplay.LevelState.color = Color.green;
        
            Debug.Log("Level complete!");
        }

        public void BossFight()
        {
            IsBossFight = true;
        }
    }
}