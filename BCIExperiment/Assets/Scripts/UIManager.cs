using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _finishedPanel;
        [SerializeField] private GameObject _enterNumberPanel;
        public delegate void StartButtonClicked();
        public static event StartButtonClicked onStartbuttonClicked;
        
        public delegate void AbortButtonClicked();
        public static event AbortButtonClicked onAbortbuttonClicked;

        public delegate void BackToMenuButtonClicked();
        public static event BackToMenuButtonClicked onBackToMenuButtonClicked;

        public delegate void ParticipantNumberEntered();
        public static event ParticipantNumberEntered onParticipantNumberEntered;

        private void OnEnable()
        {
            onBackToMenuButtonClicked += BackToMenuHandler;
            ExperimentManager.onExperimentFinished += FinishedPanelHandler;
            _finishedPanel.SetActive(false);
            _enterNumberPanel.SetActive(true);
        }

        public void StartButtonHandler()
        {
            if (onStartbuttonClicked != null)
            {
                onStartbuttonClicked();
            }
        }

        public void AbortButtonHandler()
        {
            if (onAbortbuttonClicked != null)
            {
                onAbortbuttonClicked();
            }
        }

        public void FinishButtonHandler()
        {
            if (onBackToMenuButtonClicked != null)
            {
                onBackToMenuButtonClicked();
            }
        }

        public void OKButtonHandler()
        {
            _enterNumberPanel.SetActive(false);
            if (onParticipantNumberEntered != null)
            {
                onParticipantNumberEntered();
            }
        }

        public void FinishedPanelHandler(string msg)
        {
            _finishedPanel.SetActive(true);
        }

        private void BackToMenuHandler()
        {
            SceneManager.LoadScene("Hub", LoadSceneMode.Single);
        }
    }
}