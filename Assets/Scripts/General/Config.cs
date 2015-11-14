#region

using UnityEngine;

#endregion

namespace Assets.Scripts.General
{
    public class Config : MonoBehaviour
    {
        public static Config Ctrl;
        public int Difficulty;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else
                Destroy(gameObject);
        }
    }
}