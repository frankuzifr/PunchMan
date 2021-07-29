using UnityEngine;

namespace PunchMan
{
    public class GameState : MonoBehaviour
    {
        public bool IsGameOver { get; private set; }
        public bool IsBossFight { get; private set; }

        public void StartGame()
        {
            IsGameOver = false;
        }

        public void GameOver()
        {
            IsGameOver = true;
        }

        public void BossFight()
        {
            IsBossFight = true;
        }
    }
}