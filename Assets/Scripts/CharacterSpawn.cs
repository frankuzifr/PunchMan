using UnityEngine;

namespace PunchMan
{
    public class CharacterSpawn : MonoBehaviour
    {
        private void Awake()
        {
            var levelContainer = GetComponentInParent<GameState>();
            var character = levelContainer.Character;
            Instantiate(character, transform);
        }
    }
}