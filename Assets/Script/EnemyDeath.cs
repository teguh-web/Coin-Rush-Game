using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class EnemyDeath : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.deathState = true;
                }

                // Restart scene setelah 1 detik
                Invoke("RestartScene", 1f);
            }
        }

        void RestartScene()
        {
            Time.timeScale = 1f;
            CoinController.ResetCoins();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}