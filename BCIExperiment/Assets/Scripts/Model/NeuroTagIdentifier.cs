using UnityEngine;

namespace Model
{
    public class NeuroTagIdentifier : MonoBehaviour
    {
        [SerializeField]
        private int index;

        private int countActivated;
        
        public int getIndex()
        {
            return index;
        }

        public int getCount()
        {
            return countActivated;
        }

        public void IncrementCount()
        {
            countActivated++;
        }
    }
}