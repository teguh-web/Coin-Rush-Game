using UnityEngine;

namespace Platformer
{
    public class LevelManager : MonoBehaviour
    {
        // Dipanggil saat level selesai, untuk unlock level berikutnya
        public static void UnlockNextLevel(int currentLevel)
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            if (currentLevel >= unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
                PlayerPrefs.Save();
            }
        }

        // Cek apakah level tertentu sudah terbuka
        public static bool IsLevelUnlocked(int level)
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            return level <= unlockedLevel;
        }
    }
}
