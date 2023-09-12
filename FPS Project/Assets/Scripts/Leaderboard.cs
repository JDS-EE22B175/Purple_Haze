using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    private Transform container;
    private Transform template;
    private List<Transform> highScoresTransformList;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("highScoreTable"))
        {
            HighScoresStorage defaultHighScoreStorage = new HighScoresStorage();
            HighScoreEntry devsHighScoreEntry = new HighScoreEntry { score = 1998, name = "DEVS" };
            defaultHighScoreStorage.highScoreEntryList = new List<HighScoreEntry>() { devsHighScoreEntry };
            PlayerPrefs.SetString("highScoreTable", JsonUtility.ToJson(defaultHighScoreStorage));
        }

        container = transform.Find("Entry Container");
        template = container.Find("Entry Template");

        template.gameObject.SetActive(false);

        AddHighScoreEntry(MainMenu.playerName, LogicHandler.score);

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScoresStorage highScoresStore = JsonUtility.FromJson<HighScoresStorage>(jsonString);


        for(int i = 0; i < highScoresStore.highScoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highScoresStore.highScoreEntryList.Count; j++)
            {
                if (highScoresStore.highScoreEntryList[j].score <= highScoresStore.highScoreEntryList[i].score)
                {
                    //Swap
                    HighScoreEntry temp = highScoresStore.highScoreEntryList[i];
                    highScoresStore.highScoreEntryList[i] = highScoresStore.highScoreEntryList[j];
                    highScoresStore.highScoreEntryList[j] = temp;
                }
            }
        }

        int count = 0;

        highScoresTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScoresStore.highScoreEntryList)
        {
            if (count < 8)
            {
                CreateHighScoreEntryTransform(highScoreEntry, container, highScoresTransformList);
                count++;
            }
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform entryContainer, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(template, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0f, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highScoreEntry.name;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = highScoreEntry.score.ToString();

        transformList.Add(entryTransform);
    }

    private class HighScoresStorage
    {
        public List<HighScoreEntry> highScoreEntryList;

    }

    private void AddHighScoreEntry(string name, int score)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };


        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScoresStorage highScoreStorage = JsonUtility.FromJson<HighScoresStorage>(jsonString);

        highScoreStorage.highScoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highScoreStorage);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
