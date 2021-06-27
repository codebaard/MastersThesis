using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class TargetManager : MonoBehaviour
    { 
        private List<GameObject> _neuroTags;

        public delegate void TargetSet(GameObject neuroTag);
        public static event TargetSet onTargetSet;

        public void OnEnable()
        {
            _neuroTags = new List<GameObject>();
        }

        public void Start()
        {
            _neuroTags = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
        }
        
        public void SetNewTarget()
        {
            int randomIndex = Random.Range(0, _neuroTags.Count-1);
            GameObject neurotag = _neuroTags[randomIndex];
            if (onTargetSet != null)
            {
                onTargetSet(neurotag);
            }
        }
    }
}