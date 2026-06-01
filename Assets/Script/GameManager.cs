using UnityEngine;
using TMPro; // Wajib ditambahkan untuk memanggil UI TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Membuat singleton agar mudah dipanggil
    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Awake() 
    {
        // Memastikan hanya ada satu GameManager
        instance = this;
    }

    public void AddCoin(int amount) 
    {
        coinCount += amount;
        coinText.text = "Koin: " + coinCount.ToString();
    }
}