using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static string playerName;
    public GameObject inputText;
    public GameObject playButton;

    private void Start()
    {
        LogicHandler.score = 0;
        Cursor.lockState = CursorLockMode.None;
        playButton.GetComponent<Button>().enabled = false;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReadStringInput()
    {
        playerName = inputText.GetComponent<TextMeshProUGUI>().text;
        if(playerName != null)
        {
            playButton.GetComponent<Button>().enabled = true;
        }
    }
}
