using UnityEngine;

namespace PunchMan
{
    public class Weight : MonoBehaviour
    {
        [SerializeField] private int multipliedStrength = 1;

        public int MultipliedStrength => multipliedStrength;

        public void DestroyWeight()
        {
            Destroy(gameObject);
        }
    }
}