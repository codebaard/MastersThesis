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
        private List<GameObject> _neuroTags;
        private bool _isExperimentRunning;
        [SerializeField] private int NeurotagsToSample;

        public delegate void ExperimentStarted(string message);
        public static event ExperimentStarted onExperimentStarted;

        public delegate void ExperimentFinished(string message);
        public static event ExperimentFinished onExperimentFinished;

        public delegate void TargetSet(string name);
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
                if (onExperimentFinished != null)
                {
                    onExperimentFinished("finished");
                }
            }

        }
        public void StartExperiment()
        {
            _setNewTarget();
            _isExperimentRunning = true;
            if (onExperimentStarted != null)
            {
                onExperimentStarted("started");
            }
        }
        public void AbortExperiment()
        {
            if (onExperimentFinished != null)
            {
                onExperimentFinished("aborted");
            }            
        }
        private void _setNewTarget()
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