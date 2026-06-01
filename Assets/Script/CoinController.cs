using System.Collections;
using UnityEngine;
using TMPro;

namespace Platformer
{
    public class CoinController : MonoBehaviour
    {
        [Header("Coin Settings")]
        public int coinValue = 1;

        [Header("Fade Effect Settings")]
        public float floatHeight = 1.5f;
        public float fadeDuration = 0.6f;

        [Header("References")]
        public TextMeshProUGUI coinText; // Drag CoinText dari Canvas ke sini

        public static int totalCoins = 0; // ← Dijadikan public static agar bisa di-reset
        private bool collected = false;

        void Start()
        {
            if (coinText == null)
            {
                GameObject textObj = GameObject.Find("CoinText");
                if (textObj != null)
                    coinText = textObj.GetComponent<TextMeshProUGUI>();
            }

            // Hanya reset saat pertama kali scene dimuat (bukan dari pause)
            // ResetCoins() dipanggil dari PausedManager saat Restart / GoToMainMenu
            UpdateCoinText();
        }

        // === Dipanggil oleh PausedManager.RestartLevel() dan GoToMainMenu() ===
        public static void ResetCoins()
        {
            totalCoins = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (collected) return;

            if (other.CompareTag("Player"))
            {
                collected = true;
                totalCoins += coinValue;
                UpdateCoinText();

                var finishManagers = FindObjectsByType<FinishManager>(FindObjectsSortMode.None);
                if (finishManagers.Length > 0)
                    finishManagers[0].CoinCollected();

                StartCoroutine(FadeAndFloat());
            }
        }

        private void UpdateCoinText()
        {
            if (coinText != null)
                coinText.text = "Koin: " + totalCoins;
        }

        private IEnumerator FadeAndFloat()
        {
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Destroy(gameObject);
                yield break;
            }

            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + new Vector3(0f, floatHeight, 0f);
            Color startColor = sr.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fadeDuration;
                float smoothT = t * t * (3f - 2f * t);

                transform.position = Vector3.Lerp(startPos, endPos, smoothT);
                sr.color = Color.Lerp(startColor, endColor, smoothT);

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}