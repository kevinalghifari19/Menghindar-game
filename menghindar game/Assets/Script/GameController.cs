using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public Canvas gameOverCanvas;
    public TMP_Text timerText;

    void Start()
    {
        // Sembunyikan Game Over Canvas di awal
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false);
        }

        // Subscribe ke event playerDied dari PlayerController
        if (playerController != null)
        {
            playerController.playerDied += WhenPlayerDies;
        }
    }

    // Dipanggil saat player mati
    void WhenPlayerDies()
    {
        // Tampilkan Game Over Canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
        }

        // Hentikan waktu game
        Time.timeScale = 0f;
    }

    // Fungsi untuk tombol Retry
    public void RetryClicked()
    {
        // Kembalikan waktu ke normal
        Time.timeScale = 1f;
        
        // Load ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Cleanup saat object dihancurkan
    void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.playerDied -= WhenPlayerDies;
        }
    }
}