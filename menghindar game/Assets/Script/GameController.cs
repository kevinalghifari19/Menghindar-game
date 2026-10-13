using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public Canvas gameOverCanvas;
    public TMP_Text timerText;

    private float timer = 0f;
    private bool isGameRunning = true;

    void Start()
    {
        // Sembunyikan Game Over Canvas di awal
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false);
        }

        // Subscribe ke event playerDied
        if (playerController != null)
        {
            playerController.playerDied += WhenPlayerDies;
        }
    }

    void Update()
    {
        // Hitung waktu selama game berjalan
        if (isGameRunning)
        {
            timer += Time.deltaTime;
            
            // Tampilkan timer di layar (1 angka di belakang koma)
            if (timerText != null)
            {
                timerText.text = "You Lasted: " + timer.ToString("F1");
            }
        }
    }

    // Dipanggil saat player mati
    void WhenPlayerDies()
    {
        isGameRunning = false; // Hentikan timer
        
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
        
        // Load ulang scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        // Cleanup
        if (playerController != null)
        {
            playerController.playerDied -= WhenPlayerDies;
        }
    }
}