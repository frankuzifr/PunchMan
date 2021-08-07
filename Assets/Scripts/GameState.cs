using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PunchMan
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private List<ButtonWithLevel> buttonsWithLevels;
        [SerializeField] private Camera camera;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button repeatButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private LevelButtons levelButtons;
        [SerializeField] private EndLevelDisplay endLevelDisplay;
        [SerializeField] private Character character;

        public EndLevelDisplay EndLevelDisplay => endLevelDisplay;

        public Character Character => character;

        public int NumberLevel { get; private set; }

        private void Awake()
        {
            foreach (var buttonWithLevel in buttonsWithLevels)
            {
                var button = buttonWithLevel.Button;
                var numberButton = buttonsWithLevels.IndexOf(buttonWithLevel);
                
                if (numberButton > 0)
                    button.interactable = false;
                
                var level = buttonWithLevel.Level;
                button.onClick.AddListener(
                    () =>
                    {
                        if (transform.childCount > 0)
                            Destroy(transform.GetChild(0).gameObject);
                        
                        levelButtons.gameObject.SetActive(false);
                        
                        camera.gameObject.SetActive(false);
                        Instantiate(level, transform);

                        NumberLevel = buttonsWithLevels.IndexOf(buttonWithLevel);
                    });
            }
            
            menuButton.onClick.AddListener(
                () =>
                {
                    var currentLevel = transform.GetChild(0).gameObject;
                    Destroy(currentLevel);
                    
                    camera.gameObject.SetActive(true);
                    endLevelDisplay.gameObject.SetActive(false);
                    levelButtons.gameObject.SetActive(true);
                });
            
            repeatButton.onClick.AddListener(
                () => 
                {
                    endLevelDisplay.gameObject.SetActive(false);
                    Destroy(transform.GetChild(0).gameObject);
                    Instantiate(buttonsWithLevels[NumberLevel].Level, transform);
                });
            
            nextLevelButton.onClick.AddListener(
                () => 
                {
                    endLevelDisplay.gameObject.SetActive(false);
                    Destroy(transform.GetChild(0).gameObject);
                    NumberLevel++;
                    Instantiate(buttonsWithLevels[NumberLevel].Level, transform);
                });
        }

        public bool IsLastLevel()
        {
            return NumberLevel == buttonsWithLevels.Count - 1;
        }

        public void InteractableNextButton()
        {
            buttonsWithLevels[NumberLevel + 1].Button.interactable = true;
        }
    }
}