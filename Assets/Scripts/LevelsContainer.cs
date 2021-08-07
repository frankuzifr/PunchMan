using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PunchMan
{
    public class LevelsContainer : MonoBehaviour
    {
        [SerializeField] private List<ButtonWithLevel> buttonsWithLevels;
        [SerializeField] private Camera camera;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button repeatButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private LevelButtons levelButtons;
        [SerializeField] private EndLevelDisplay endLevelDisplay;

        public EndLevelDisplay EndLevelDisplay => endLevelDisplay;

        private void Awake()
        {
            foreach (var buttonWithLevel in buttonsWithLevels)
            {
                var button = buttonWithLevel.Button;
                var level = buttonWithLevel.Level;
                button.onClick.AddListener(
                    () =>
                    {
                        if (transform.childCount > 0)
                            Destroy(transform.GetChild(0).gameObject);
                        levelButtons.gameObject.SetActive(false);
                        
                        camera.gameObject.SetActive(false);
                        
                        Instantiate(level, transform);
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
                    var currentLevel = transform.GetChild(0).gameObject;
                    Destroy(transform.GetChild(0).gameObject);
                    
                    
                    var index = 0;
                    foreach (var buttonsWithLevel in buttonsWithLevels)
                    {
                        if (buttonsWithLevel.Level != currentLevel) 
                            continue;

                        index = buttonsWithLevels.IndexOf(buttonsWithLevel);
                        break;
                    }
                    endLevelDisplay.gameObject.SetActive(false);
                    Destroy(transform.GetChild(0).gameObject);
                    Instantiate(buttonsWithLevels[index].Level, transform);
                });
            
            nextLevelButton.onClick.AddListener(
                () => 
                {
                    var currentLevel = transform.GetChild(0).gameObject;

                    var index = 0;
                    foreach (var buttonsWithLevel in buttonsWithLevels)
                    {
                        if (buttonsWithLevel.Level != currentLevel) 
                            continue;

                        index = buttonsWithLevels.IndexOf(buttonsWithLevel);
                        break;
                    }
                    endLevelDisplay.gameObject.SetActive(false);
                    Destroy(transform.GetChild(0).gameObject);
                    Instantiate(buttonsWithLevels[index + 1].Level, transform);
                });
        }
    }
}