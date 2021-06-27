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

            TargetManager.onTargetSet += ActivationHandler;
            ExperimentManager.onExperimentFinished += onDeactivated;
        }

        private void OnDisable()
        {
            TargetManager.onTargetSet -= ActivationHandler;
            ExperimentManager.onExperimentFinished -= onDeactivated;
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

        public void ActivationHandler(GameObject neurotag)
        {
            NeuroTagLogWrapper tag = neurotag.GetComponent<NeuroTag>().GetComponent<NeuroTagLogWrapper>();
            if (tag.Equals(this))
                onActivated("is active");
            else
                onDeactivated("deactivated");
        }

        public void onActivated(string msg)
        {
            _neuroTag.onConfidenceChanged.AddListener(OnConfidenceChanged);
            _neuroTag.onTriggered.AddListener(OnTriggered);
        }

        public void onDeactivated(string msg)
        {
            _neuroTag.onConfidenceChanged.RemoveListener(OnConfidenceChanged);
            _neuroTag.onTriggered.RemoveListener(OnTriggered);
        }
        public int getNeuroTagIndex(){
            return _identifier.getIndex();
        }
    }

}