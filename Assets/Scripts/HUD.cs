#region

using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts
{
    public class HUD : MonoBehaviour
    {
        public static HUD Ctrl;
        private Dungeon.Dungeon _dungeon;
        public GameObject ChunkPrefab;
        //Debugging
        public bool Debugg = true;
        public GameObject DoorPrefab;
        public GameObject EnemyPrefab;
        public GameObject FloorReference;
        public GameObject GameOverPanel;
        public Text GameOverScoreLabel;
        public Text HighScoreLabel;
        public bool IsPaused;
        public float LastRotation;
        public GameObject PlatformPrefab;
        public HeroBehaviour Player;
        public GameObject RoofReference;
        public Text ScoreLabel;
        public Chunk TestChunk;

        public void Awake()
        {
            if (Ctrl == null)
            {
                Ctrl = this;
            }
            else if (Ctrl != this)
            {
                Destroy(gameObject);
            }
            
        }

        public void Start()
        {
            _dungeon = new Dungeon.Dungeon(ChunkPrefab, PlatformPrefab, DoorPrefab, EnemyPrefab,
                FloorReference.transform.position,
                RoofReference.transform.position, 250);
            _dungeon.SetDifficulty(Config.Ctrl.Difficulty);

            IsPaused = false;
            UnPauseGame();
            GameOverPanel.SetActive(false);
        }

        public void Update()
        {
            ScoreLabel.text = "Score: " + Player.Score;
            HighScoreLabel.text = "High Score: " + PersistentData.Ctrl.GetHighestScore();
            if (!Player.IsAlive)
            {
                OnPlayerDeath();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            if (!IsPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            IsPaused = true;
        }

        private void UnPauseGame()
        {
            Time.timeScale = 1;
            IsPaused = false;
        }

        private void OnPlayerDeath()
        {
            var playerScore = Player.Score;
            PauseGame();
            GameOverPanel.SetActive(true);
            if (PersistentData.Ctrl.SetHighestScore(playerScore))
            {
                GameOverScoreLabel.text = "New Highest Score:\n" + playerScore + "!!!";
            }
            else
            {
                GameOverScoreLabel.text = "Your Score:\n" + playerScore;
            }
        }

        public int GetScore()
        {
            return Player.Score;
        }
    }
}