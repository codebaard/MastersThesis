using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NextMind;
using NextMind.NeuroTags;
using TMPro.EditorUtilities;
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
            List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
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