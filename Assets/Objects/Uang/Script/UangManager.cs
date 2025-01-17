using UnityEngine;
using UnityEngine.UI;

public class UangManager : MonoBehaviour
{
    public static UangManager instance;
    public Text scoreText;
    private int score = 0;
    private GameObject player;
  

    void Awake (){
    }
    private void Start()
    {
        // Load nilai uang dari PlayerPrefs saat game dimulai
        score = PlayerPrefs.GetInt("TotalUang", 0);
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Uang: Rp" + score;

        // Simpan nilai uang ke PlayerPrefs setiap kali bertambah
        PlayerPrefs.SetInt("TotalUang", score);
        PlayerPrefs.Save(); // Pastikan untuk menyimpan perubahan
    }

    // Method untuk memperbarui UI teks
    private void UpdateScoreText()
    {
        scoreText.text = "Uang  : Rp" + score.ToString();
    }

    // Method untuk Reset nilai uang
    [ContextMenu("Reset Uang")]
    public void ResetUang()
    {
        score = 0;
        PlayerPrefs.SetInt("TotalUang", score);
        PlayerPrefs.Save();
        UpdateScoreText();
    }
}
