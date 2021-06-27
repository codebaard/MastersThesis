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
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            TargetManager.onTargetSet += ActivationHandler;
        }
        
        public void ActivationHandler(GameObject neurotag)
        {
            Highlighter highlighter = neurotag.GetComponentInChildren<Highlighter>();
            if (highlighter.Equals(this))
                onActivated("is active");
            else
                onDeactivated("deactivated");
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