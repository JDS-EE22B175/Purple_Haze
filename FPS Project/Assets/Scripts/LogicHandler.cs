using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LogicHandler : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI mageCountText;
    public TextMeshProUGUI pauseText;
    public Image pauseScreen;
    public int mageCount;
    public GameObject resumeButton;
    public GameObject exitButton;
    public bool isPaused = false;
    public static int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        pauseText.enabled = false;
        pauseScreen.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        score = Mathf.RoundToInt(Time.time * 60);
        if (mageCount == 1) mageCountText.text = mageCount + " Enemy Left";
        else mageCountText.text = mageCount + " Enemies Left";

        timeText.text = "TIME: " + score.ToString();

        if (Input.GetKeyDown(KeyCode.Space) && !isPaused) PauseGame();
        else if (Input.GetKeyDown(KeyCode.Space) && isPaused) ContinueGame();

        if (Input.GetKeyDown(KeyCode.Escape)) ExitGame();

        if (mageCount <= 0)
        {
            StartCoroutine(WinSequence());
        }
    }

    public void PauseGame()
    {
        pauseScreen.enabled = true;
        pauseText.enabled = true;
        Time.timeScale = 0f;
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
        Debug.Log("Paused");
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        pauseScreen.enabled = false;
        pauseText.enabled = false;
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Resumed");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
    }
}
