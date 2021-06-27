using System;
using DefaultNamespace;
using NextMind.NeuroTags;
using UnityEngine;

namespace Model
{
    public class NeuroTagLogWrapper : MonoBehaviour
    {

        [SerializeField] private LogManager _logManager;
        
        private NeuroTagIdentifier _identifier;
        private NeuroTag _neuroTag;

        public void Start()
        {
            _neuroTag = gameObject.GetComponent<NeuroTag>();
            _identifier = gameObject.GetComponent<NeuroTagIdentifier>();

            _neuroTag.onConfidenceChanged.AddListener(OnConfidenceChanged);
            _neuroTag.onTriggered.AddListener(OnTriggered);
            ExperimentManager.onExperimentFinished += onExperimentFinished;
        }
        
        public void OnTriggered()
        {
            _logManager.PostDataToLogfile(new NeuroTagHitLogEntry(_identifier.getIndex()));
        }
        public void OnConfidenceChanged(float value)
        {
            if(_logManager.LogConfidenceData())
                    _logManager.PostDataToLogfile(new NeuroTagConfidenceLogEntry(value, _identifier.getIndex()));
        }

        public void onExperimentFinished(string msg)
        {
            _neuroTag.onConfidenceChanged.RemoveListener(OnConfidenceChanged);
            _neuroTag.onTriggered.RemoveListener(OnTriggered);
        }
        public int getNeuroTagIndex(){
            return _identifier.getIndex();
        }
    }

}