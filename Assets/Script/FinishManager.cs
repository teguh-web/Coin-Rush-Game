using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Platformer
{
    public class FinishManager : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject finishPanel;
        public TextMeshProUGUI finishText;

        [Header("Settings")]
        public int currentLevelNumber = 1; // Isi sesuai nomor level (Level 1 = 1, Level 2 = 2, dst)
        public string nextSceneName = "Level 2";
        public float delayBeforeShow = 1f;

        private int totalCoins;
        private int collectedCoins = 0;

        void Start()
        {
            totalCoins = FindObjectsByType<CoinController>(FindObjectsSortMode.None).Length;
            if (finishPanel != null)
                finishPanel.SetActive(false);
        }

        public void CoinCollected()
        {
            collectedCoins++;
            if (collectedCoins >= totalCoins)
                StartCoroutine(ShowFinish());
        }

        private IEnumerator ShowFinish()
        {
            yield return new WaitForSeconds(delayBeforeShow);
            if (finishPanel != null)
                finishPanel.SetActive(true);
            if (finishText != null)
                finishText.text = "Level Complete!\nSemua Koin Terkumpul!";

            // Unlock level berikutnya
            LevelManager.UnlockNextLevel(currentLevelNumber);

            Time.timeScale = 0f;
        }

        public void GoToNextLevel()
        {
            Time.timeScale = 1f;
            CoinController.ResetCoins();
            SceneManager.LoadScene(nextSceneName);
        }

        public void RestartLevel()
        {
            Time.timeScale = 1f;
            CoinController.ResetCoins();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            CoinController.ResetCoins();
            SceneManager.LoadScene("mainmenu");
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}