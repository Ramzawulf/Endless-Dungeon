#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class GameController : MonoBehaviour
    {
        public static GameController Ctrl;
        private bool _pause;
        public float ChunkLifeTime = 0.8f;
        public Vector3 FloorReference;
        public Vector3 HeroStartPostion;
        public Vector2 RoofReference;

        public bool IsPaused
        {
            get { return _pause; }
            set { }
        }

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            //Hero & Camera Setup
            var cb = Camera.main.GetComponent<CameraBehaviour>();
            cb.target = HeroBehaviour.Ctrl.transform;
        }

        public void Update()
        {
        }

        public void GameOver()
        {
            GameOverUIPanelManager.Ctrl.Show();
        }

        public void TogglePause()
        {
            Time.timeScale = Time.timeScale < 1 ? 1 : 0;
        }
    }
}