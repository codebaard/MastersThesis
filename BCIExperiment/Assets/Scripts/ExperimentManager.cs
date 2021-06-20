using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NextMind;
using NextMind.NeuroTags;

namespace DefaultNamespace
{
    public class ExperimentManager : MonoBehaviour
    {
        private List<GameObject> neuroTags;
        private bool _isExperimentRunning;
        [SerializeField] private int NeurotagsToSample;

        public ExperimentManager()
        {
            neuroTags = GameObject.FindGameObjectsWithTag("Neurotag").ToList();
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

        public void onExperimentStarted()
        {
            _isExperimentRunning = true;
            _setNewTarget();
        }

        public Event ExperimentFinished()
        {
            print("done");
            return null;
        }

        private void _setNewTarget()
        {
            
        }
    }
}