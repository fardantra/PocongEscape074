using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinMenu : MonoBehaviour
{
    public GameObject[] skins; // Daftar skin yang tersedia
    public GameObject glow;
    private int selectedSkinIndex = 0;

    private void Start()
    {
        // Ambil skin yang disimpan sebelumnya (default 0 jika belum disimpan)
        selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        ApplySkin();
    }

    // Fungsi untuk memilih skin berdasarkan tombol
    public void SelectSkin(int index)
    {
        selectedSkinIndex = index;

        // Simpan pilihan skin ke PlayerPrefs agar tetap tersimpan
        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
        PlayerPrefs.Save();

        // Terapkan skin
        ApplySkin();
    }

    public void ApplySkin()
    {
        // Nonaktifkan semua skin, lalu aktifkan skin yang dipilih
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }
        skins[selectedSkinIndex].SetActive(true);
        
        //Glow to selected skin
        if (selectedSkinIndex == 0)
        {
            glow.transform.position = new Vector2(-3.2f, 0f);
        }
        else if (selectedSkinIndex == 1)
        {
            glow.transform.position = new Vector2(-1f, 0f);
        }
        else if (selectedSkinIndex == 2)
        {
            glow.transform.position = new Vector2(1.3f, 0f);
        }
        else if (selectedSkinIndex == 3)
        {
            glow.transform.position = new Vector2(3.8f, 0f);
        }

    }

    // Fungsi untuk memulai game dengan skin yang dipilih
    public void StartGame()
    {
        // Lanjutkan ke scene game
        SceneManager.LoadScene("GameScene");
    }
}
