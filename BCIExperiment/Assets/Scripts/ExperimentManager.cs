using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NextMind;
using NextMind.NeuroTags;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class ExperimentManager : MonoBehaviour
    {
        private static System.Random rnd = new System.Random(); //Use explicitly system library
        private List<GameObject> _neuroTags;
        private bool _isExperimentRunning;
        [SerializeField] private int NeurotagsToSample;

        public delegate void ExperimentStarted();
        public static event ExperimentStarted onExperimentStarted;

        public delegate void TargetSet(int index);
        public static event TargetSet onTargetSet;

        public void Start()
        {
            _neuroTags = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
        }

        public ExperimentManager()
        {
            _isExperimentRunning = false;
        }

        public void OnTriggered()
        {
            if (NeurotagsToSample > 0)
            {
                _setNewTarget();
                NeurotagsToSample--;               
            }
            else
            {
                ExperimentFinished();
            }

        }

        public void StartExperiment()
        {
            Debug.Log("Experiment started");
            _isExperimentRunning = true;
            _setNewTarget();
            if (onExperimentStarted != null)
            {
                onExperimentStarted();
            }
        }

        public Event ExperimentFinished()
        {
            print("done");
            return null;
        }

        private void _setNewTarget()
        {
            int randomIndex = rnd.Next(_neuroTags.Count);
            GameObject neurotag = _neuroTags[randomIndex];
            string name = neurotag.name;
            int index = Convert.ToInt16(name[name.Length - 1]);
            
            if (onTargetSet != null)
            {
                onTargetSet(index);
            }
        }
    }
}