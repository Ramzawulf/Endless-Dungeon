#region

using UnityEngine;

#endregion

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public Audio Audio;
        // Use this for initialization
        private void Start()
        {
            Audio.playIntro();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnGUI()
        {
        }

        public void GoToGame()
        {
            ClearData();
            Application.LoadLevel("Game");
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}