using UnityEngine;

namespace PunchMan
{
    public class LevelState : MonoBehaviour
    {
        public bool IsGameOver { get; private set; }
        public bool IsLevelComplete { get; private set; }
        public bool IsBossFight { get; private set; }

        public void GameOver()
        {
            IsGameOver = true;
            Debug.Log("Level failed!");
        } 
        
        public void LevelComplete()
        {
            IsLevelComplete = true;
            Debug.Log("Level complete!");
        }

        public void BossFight()
        {
            IsBossFight = true;
        }
    }
}