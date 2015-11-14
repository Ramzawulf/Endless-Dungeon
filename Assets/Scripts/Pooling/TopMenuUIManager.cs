#region

using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Pooling
{
    public class TopMenuUIManager : MonoBehaviour
    {
        public static TopMenuUIManager Ctrl;
        public float CurrentScore {get { return HeroBehaviour.Ctrl.Score; } set {} }
        public Text CurrentScoreText;
        public Text HighScoreText;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if(Ctrl != this)
                Destroy(this);
        }

        public void Start()
        {
            HighScoreText.text = "High Score: " + PersistentData.Ctrl.GetHighestScore();
        }

        // Update is called once per frame
        public void Update()
        {
            CurrentScoreText.text = "Score: " + HeroBehaviour.Ctrl.Score;
        }

        public void DisplaySettings()
        {
            print("display settings");
        }
    }
}