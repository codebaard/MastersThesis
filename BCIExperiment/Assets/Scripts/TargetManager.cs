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
        private List<NeuroTag> _neuroTags;
        
        public delegate void TargetSet(string name);
        public static event TargetSet onTargetSet;

        public void OnEnable()
        {
            _neuroTags = new List<NeuroTag>();
        }

        public void Start()
        {
            List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
            foreach (GameObject go in gameObjects)
            {
                _neuroTags.Add(go.GetComponent<NeuroTag>());
            }
        }
        
        public void SetNewTarget()
        {
            int randomIndex = Random.Range(0, _neuroTags.Count-1);
            NeuroTag neurotag = _neuroTags[randomIndex];
            string name = neurotag.name;
            if (onTargetSet != null)
            {
                onTargetSet(name);
            }
        }
    }
}