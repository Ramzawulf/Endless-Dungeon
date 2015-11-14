#region

using System.Linq;
using Assets.Scripts.General;
using UnityEngine;

#endregion

namespace Assets.Scripts.Main_Menu
{
    public class PressStart : MonoBehaviour
    {
        public void Update()
        {
            //PersistentData.Ctrl.Clear();
            if (Input.touchSupported)
            {
                if (Input.touches.Any())
                    Application.LoadLevel("Pooling");
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Application.LoadLevel("Pooling");
            }
        }
    }
}