using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class TargetManager : MonoBehaviour
    { 
        private List<GameObject> _neuroTags;
        private Targets _targets;
        public delegate void TargetSet(GameObject neuroTag);
        public static event TargetSet onTargetSet;

        public void OnEnable()
        {
            _neuroTags = new List<GameObject>();
        }

        public void Start()
        {
            _neuroTags = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
            _targets = new Targets();
        }
        
        public void SetNewTarget()
        {
            // int randomIndex = Random.Range(0, _neuroTags.Count-1);
            int randomIndex = _targets.getNextTarget();
            GameObject neurotag = _neuroTags[randomIndex];
            if (onTargetSet != null)
            {
                onTargetSet(neurotag);
            }
        }
    }

    public class Targets
    {
        private List<int> _targetOrder;
        public Targets()
        {
            _targetOrder = new List<int>(){9, 8, 7, 1, 9, 3, 8, 9, 2, 1, 9, 4, 8, 4, 6, 0, 9, 8, 6, 3, 1, 2, 6, 8, 0, 1, 2, 6, 5, 6, 8, 4, 9, 1, 5, 8, 9, 3, 5, 1, 8, 3, 4, 0, 1, 8, 1, 2, 9, 8, 3};
        }
        public int getNextTarget()
        {
            return pop();
        }

        private int pop()
        {
            int target = _targetOrder[0];
            _targetOrder.RemoveAt(0);
            return target;
        }
    }
}