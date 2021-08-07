using TMPro;
using UnityEngine;

namespace PunchMan
{
    public class EndLevelDisplay : MonoBehaviour
    {
        public NextLevel NextLevel { get; private set; }
        public TMP_Text LevelState { get; private set; }

        private void Awake()
        {
            NextLevel = GetComponentInChildren<NextLevel>();
            LevelState = GetComponentInChildren<TMP_Text>();
        }
    }
}