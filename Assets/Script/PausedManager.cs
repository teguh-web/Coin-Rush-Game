using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PausedManager : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject pausePanel;

        private bool isPaused = false;

        void Start()
        {
            if (pausePanel != null)
                pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }

        // === BtnPaused → OnClick ===
        public void PauseGame()
        {
            isPaused = true;
            if (pausePanel != null)
                pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // === BtnCnc → OnClick ===
        public void ResumeGame()
        {
            isPaused = false;
            if (pausePanel != null)
                pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        // === BtnRestart → OnClick ===
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            isPaused = false;
            CoinController.ResetCoins(); // ← Reset koin ke 0 sebelum reload
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // === BtnHome → OnClick ===
        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            isPaused = false;
            CoinController.ResetCoins(); // ← Reset koin juga saat ke main menu
            SceneManager.LoadScene("mainmenu");
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}