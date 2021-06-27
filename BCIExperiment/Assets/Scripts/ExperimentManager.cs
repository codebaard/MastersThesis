using UnityEngine;

namespace DefaultNamespace
{
    public class ExperimentManager : MonoBehaviour
    {
        [SerializeField]
        private TargetManager _targetManager;
        private bool _isExperimentRunning;
        [SerializeField] private int _neurotagsToSample;

        public delegate void ExperimentStarted(string message);
        public static event ExperimentStarted onExperimentStarted;

        public delegate void ExperimentFinished(string message);
        public static event ExperimentFinished onExperimentFinished;

        private void OnEnable()
        {
            _isExperimentRunning = false;
            UIManager.onStartbuttonClicked += StartExperiment;
            UIManager.onAbortbuttonClicked += AbortExperiment;
        }

        private void OnDisable()
        {
            UIManager.onStartbuttonClicked -= StartExperiment;  
            UIManager.onAbortbuttonClicked -= AbortExperiment;
        }

        public void OnTriggered()
        {
            if (_neurotagsToSample > 0 && _isExperimentRunning)
            {
                _targetManager.SetNewTarget();
                _neurotagsToSample--;               
            }
            else if (_isExperimentRunning)
            {
                if (onExperimentFinished != null)
                {
                    onExperimentFinished("finished");
                }

                _isExperimentRunning = false;
            }
        }
        public void StartExperiment()
        {
            _targetManager.SetNewTarget();
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
    }
}