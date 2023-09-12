using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = "YOUR TIME " + LogicHandler.score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
