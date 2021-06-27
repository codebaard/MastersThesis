using System;
using Model;
using NextMind.NeuroTags;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Highlighter : MonoBehaviour
    {
        private Animator _animator;
        private GameObject _parentNeurotag;
        
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            TargetManager.onTargetSet += ActivationHandler;
            ExperimentManager.onExperimentFinished += OnFinishedHandler;
        }

        private void OnDisable()
        {
            TargetManager.onTargetSet -= ActivationHandler;
            ExperimentManager.onExperimentFinished -= OnFinishedHandler;
        }

        public void ActivationHandler(GameObject neurotag)
        {
            Highlighter highlighter = neurotag.GetComponentInChildren<Highlighter>();
            if (highlighter.Equals(this))
                onActivated("is active");
            else
                onDeactivated("deactivated");
        }

        private void OnFinishedHandler(string msg)
        {
            _animator.SetFloat("ConfidenceValue",0);
        }

        private void onActivated(string msg)
        {
            _animator.SetFloat("ConfidenceValue",1);
        }

        private void onDeactivated(string msg)
        {
            _animator.SetFloat("ConfidenceValue",0);
        }
    }
}