using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class TargetManager : MonoBehaviour
    { 
        private List<GameObject> _neuroTags;
        
        public delegate void TargetSet(string name);
        public static event TargetSet onTargetSet;        
        
        public void Start()
        {
            _neuroTags = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
        }
        
        public void SetNewTarget()
        {
            int randomIndex = Random.Range(0, _neuroTags.Count-1);
            GameObject neurotag = _neuroTags[randomIndex];
            string name = neurotag.name;
            
            if (onTargetSet != null)
            {
                onTargetSet(name);
            }
        }
    }
}