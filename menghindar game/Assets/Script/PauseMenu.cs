using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;

    // Penanda apakah game sudah over (diatur oleh GameController)
    public static bool isGameOver = false;
    private bool isPaused = false;

    void Awake()
    {
        // Reset semua kondisi setiap game dimulai
        Time.timeScale = 1f;
        isGameOver = false;
        isPaused = false;

        if (container != null)
        {
            container.SetActive(false);
        }
    }

    void Update()
    {
        // Jika sudah mati, ESC tidak berfungsi sama sekali
        if (isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (container != null) container.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        isPaused = false;
        if (container != null) container.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}