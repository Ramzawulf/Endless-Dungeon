#region

using UnityEngine;

#endregion

namespace Assets.Scripts.General
{
    public class PersistentData : MonoBehaviour
    {
        public static PersistentData Ctrl;
        private int _highestLevel;
        private int _highestScore;
        private string _playerName;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if(Ctrl != this)
                Destroy(gameObject);
            Load();
        }
        
        public void Save()
        {
            PlayerPrefs.Save();
        }

        public void Load()
        {
            _highestScore = PlayerPrefs.GetInt(DataKeys.HighestScore);
            _highestLevel = PlayerPrefs.GetInt(DataKeys.HighestLevel);
            _playerName = PlayerPrefs.GetString(DataKeys.PlayerName);
        }

        public int GetHighestScore()
        {
            return _highestScore;
        }

        public bool SetHighestScore(int newScore)
        {
            if (newScore <= _highestScore) return false;

            PlayerPrefs.SetInt(DataKeys.HighestScore, newScore);
            return true;
        }

        public int GetHighestLevel()
        {
            return _highestLevel;
        }

        public string GetPlayerName()
        {
            return _playerName;
        }

        public class DataKeys
        {
            public const string HighestScore = "highestScore";
            public const string HighestLevel = "highestLevel";
            public const string PlayerName = "playerName";
        }

        public void Clear()
        {
            PlayerPrefs.SetInt(DataKeys.HighestScore, 0);
            PlayerPrefs.SetInt(DataKeys.HighestScore, 0);
            PlayerPrefs.SetString(DataKeys.PlayerName, "");
        }
    }
}