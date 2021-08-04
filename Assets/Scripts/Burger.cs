using UnityEngine;

namespace PunchMan
{
    public class Burger : MonoBehaviour
    {
        [SerializeField] private int multipliedStrength = 1;

        public int MultipliedStrength => multipliedStrength;

        public void DestroyBurger()
        {
            Destroy(gameObject);
        }
    }
}