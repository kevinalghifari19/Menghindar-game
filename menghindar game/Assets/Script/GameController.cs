using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject gameOverCanvas;   // ← diubah dari Canvas ke GameObject
    public TMP_Text timerText;

    private float timer = 0f;
    private bool isGameRunning = true;

    void Start()
    {
        // Reset kondisi setiap game dimulai
        Time.timeScale = 1f;
        PauseMenu.isGameOver = false;
        timer = 0f;
        isGameRunning = true;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.playerDied += WhenPlayerDies;
        }
    }

    void Update()
    {
        if (isGameRunning)
        {
            timer += Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = "You Lasted: " + timer.ToString("F1");
            }
        }
    }

    void WhenPlayerDies()
    {
        isGameRunning = false;
        PauseMenu.isGameOver = true;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RetryClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.playerDied -= WhenPlayerDies;
        }
    }
}