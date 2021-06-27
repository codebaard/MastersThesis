using UnityEngine;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        public delegate void StartButtonClicked();
        public static event StartButtonClicked onStartbuttonClicked;
        
        public delegate void AbortButtonClicked();
        public static event AbortButtonClicked onAbortbuttonClicked;

        public delegate void FinishButtonClicked();
        public static event FinishButtonClicked onFinishButtonClicked;
        
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
            if (onFinishButtonClicked != null)
            {
                onFinishButtonClicked();
            }
        }
    }
}