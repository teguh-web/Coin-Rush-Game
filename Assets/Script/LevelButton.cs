using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class LevelButton : MonoBehaviour
    {
        [Header("Settings")]
        public int levelNumber;          // Isi sesuai nomor level (1, 2, 3, dst)
        public string sceneName;         // Nama scene yang akan dibuka (Level 1, Level 2, dst)

        [Header("Visual")]
        public GameObject lockIcon;      // (Opsional) icon gembok jika terkunci

        private Button button;

        void Start()
        {
            button = GetComponent<Button>();
            RefreshState();
        }

        void OnEnable()
        {
            RefreshState(); // Refresh setiap kali panel pilih level dibuka
        }

        void RefreshState()
        {
            bool unlocked = LevelManager.IsLevelUnlocked(levelNumber);

            if (button != null)
                button.interactable = unlocked;

            if (lockIcon != null)
                lockIcon.SetActive(!unlocked);
        }

        public void OnLevelSelected()
        {
            if (LevelManager.IsLevelUnlocked(levelNumber))
                SceneManager.LoadScene(sceneName);
        }
    }
}