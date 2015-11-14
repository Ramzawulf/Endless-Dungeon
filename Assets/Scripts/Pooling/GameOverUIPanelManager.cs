#region

using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Pooling
{
    public class GameOverUIPanelManager : MonoBehaviour
    {
        public static GameOverUIPanelManager Ctrl;
        public Button BackButton;
        public Text FinalScoreText;
        public Text GameOverLabeText;
        public Button RetryButton;
        // Use this for initialization

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
            Hide();
        }

        private void Hide()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        public void Show()
        {
            if (PersistentData.Ctrl.GetHighestScore() < HeroBehaviour.Ctrl.Score)
            {
                PersistentData.Ctrl.SetHighestScore(HeroBehaviour.Ctrl.Score);
                PersistentData.Ctrl.Save();
                FinalScoreText.text = "New Best ScoRe:" + HeroBehaviour.Ctrl.Score;
            }
            else
            {
                FinalScoreText.text = "Your ScoRe: " + HeroBehaviour.Ctrl.Score;
            }


            Time.timeScale = 0;
            var audioSource = Camera.main.GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.mute = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void Retry()
        {
            var loadedLevel = Application.loadedLevel;
            PersistentData.Ctrl.Save();
            Time.timeScale = 1;
            Application.LoadLevel(loadedLevel);
        }

        public void GoBack()
        {
            PersistentData.Ctrl.Save();
            Time.timeScale = 1;
            Application.LoadLevel("MainMenu");
        }
    }
}