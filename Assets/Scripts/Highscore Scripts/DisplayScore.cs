using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DisplayScore : MonoBehaviour
{
    public List<int> scoreTable = new List<int>();

    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;

    List<GameObject> UI_elements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        entryTemplate.gameObject.SetActive(false);
        HighScoreScript.instance.readFromJson();
        addScore();
        HighScoreScript.instance.readFromJson();
        Transform[] kids = new Transform[2];

        int kidCounter = 0;

        float templateHeight = 70.0f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(40, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            foreach (Transform kid in entryTransform)
            {
                kids[kidCounter] = kid;
                kidCounter++;

            }

            kidCounter = 0;

            kids[1].GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();

            try
            {
                kids[0].GetComponent<TMPro.TextMeshProUGUI>().text =
                    (HighScoreScript.instance.top10Score[i]).ToString();
            }
            catch
            {
                kids[0].GetComponent<TMPro.TextMeshProUGUI>().text = "0";
            }


        }
    }
    void addScore()
    {
        int additonalScore = 0;

        for (int i = 0; i < scoreTable.Count; i++)
        {
            if (scoreTable[i] <= HighScoreScript.instance.currentScore)
            {
                additonalScore = i;
                break;
            }
        }

        scoreTable.Add(HighScoreScript.instance.currentScore);
        scoreTable.Sort();

        if (scoreTable.Count > 10)
        {
            scoreTable.RemoveRange(scoreTable[scoreTable.Count - 1], 0);
        }

        HighScoreScript.instance.addScore();
    }

}
