#region

using UnityEngine;

#endregion

namespace Assets.Scripts.General
{
    public class NavigationHelper : MonoBehaviour
    {
        public void Retry()
        {
            PersistentData.Ctrl.Save();
            //UnPauseGame();
            Application.LoadLevel("Game");
        }

        public void GoToMainMenu()
        {
            PersistentData.Ctrl.Save();
            //UnPauseGame();
            Application.LoadLevel("MainMenu");
        }
    }
}