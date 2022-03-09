using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
[System.Serializable]
public class playerData
{
    public List<int> score;
}
public class HighScoreScript : MonoBehaviour
{


        public List<int> highScoreTable;
        public int currentScore;
        public List<int> top10Score;
        public List<int> tempScores = new List<int>();

        public static HighScoreScript instance;

        // Start is called before the first frame update
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        void Start()
        {
            readFromJson();
            addScore();
        }

        public void addScore()
        {
            playerData playerScore = new playerData();

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    highScoreTable.Add(tempScores[i]);
                }
                catch
                {
                    break;
                }
            }

            highScoreTable.Add(currentScore);
            highScoreTable.Sort();
            highScoreTable.Reverse();


            playerScore.score = highScoreTable;

            var path = System.IO.Path.Combine(Application.persistentDataPath, Application.persistentDataPath + "/text.json");

            string json = JsonUtility.ToJson(playerScore);

            Debug.Log(Application.persistentDataPath);

            File.WriteAllText(path, json);
        }

        public void readFromJson()
        {
            if (File.Exists(Application.persistentDataPath + "/text.json"))
            {
                string data = File.ReadAllText(Application.persistentDataPath + "/text.json");

                playerData playerScore = new playerData();

                playerScore = JsonUtility.FromJson<playerData>(data);

                highScoreTable = playerScore.score;

                top10Score = new List<int>(highScoreTable);

                foreach (int num in top10Score)
                {
                    Debug.Log(num);
                }
                Debug.Log("READING FINISHED.");
            }
        }
    
}
