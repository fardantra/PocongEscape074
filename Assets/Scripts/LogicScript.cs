using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject UangUI;
    public GameObject ScoreUI;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider bgmSliderPause;
    public Slider sfxSliderPause;
    public GameObject HighScoreUI;
    public GameObject gameOverScreen;
    public GameObject gameStartScreen;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject skinsMenu;
    public GameObject settingsMenu;
    public GameObject settingsMenuPause;
    public static bool sceneRolling; // To move the background endlessly
    public static bool playerIsAlive = false; // Is the player alive? 
    public LogicScript logic;
    AudioManager audioManager;

    private void Awake()
    {
        UangUI.SetActive(false); // make the Uang UI apprears when the game started
        ScoreUI.SetActive(false); // make the score UI apprears when the game started
        HighScoreUI.SetActive(false);
        settingsMenu.SetActive(false);
        player.SetActive(false);
        sceneRolling = true;
        gameStartScreen.SetActive(true);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Set slider value dari PlayerPrefs atau gunakan nilai default 0.5
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        bgmSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;
        bgmSliderPause.value = savedBGMVolume;
        sfxSliderPause.value = savedSFXVolume;

        // Tambahkan listener ke semua slider
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            SyncSliders(value, bgmSliderPause);
            audioManager.SetBGMVolume(value);
        });

        bgmSliderPause.onValueChanged.AddListener((value) =>
        {
            SyncSliders(value, bgmSlider);
            audioManager.SetBGMVolume(value);
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            SyncSliders(value, sfxSliderPause);
            audioManager.SetSFXVolume(value);
        });

        sfxSliderPause.onValueChanged.AddListener((value) =>
        {
            SyncSliders(value, sfxSlider);
            audioManager.SetSFXVolume(value);
        });

        skinsMenu.SetActive(false);
    }

        // Fungsi untuk sinkronisasi slider
    private void SyncSliders(float value, Slider targetSlider)
    {
        if (targetSlider.value != value)
        {
            targetSlider.value = value;
        }
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        sceneRolling = false;
        audioManager.GameOverAudio();
        pauseMenu.SetActive(false);
        playerIsAlive = false;
    }

    public void gameStart()
    {
        UangUI.SetActive(true); // make the Uang UI apprears when the game started
        ScoreUI.SetActive(true); // make the score UI apprears when the game started
        HighScoreUI.SetActive(true);
        gameStartScreen.SetActive(false);
        player.SetActive(true);
        playerIsAlive = true;
    }

    public void selectSkins ()
    {
        skinsMenu.SetActive(true);
        gameStartScreen.SetActive(false);
        
        SkinMenu skinMenu = skinsMenu.GetComponent<SkinMenu>();
        if (skinMenu != null)
        {
            skinMenu.ApplySkin(); // Terapkan skin terakhir yang dipilih
        }
    }

    public void homeButton() 
    {
        skinsMenu.SetActive(false);
        gameStartScreen.SetActive(true);
    }
    
    public void doExitGame() 
    {
        Application.Quit();
    }

        // Fungsi untuk membuka Settings Menu
    public void openSettings()
    {
        UangUI.SetActive(false); 
        ScoreUI.SetActive(false); 
        HighScoreUI.SetActive(false);
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false); // Menyembunyikan pauseMenu jika ada
        gameStartScreen.SetActive(false);
    }

    // Fungsi untuk menutup Settings Menu
    public void closeSettings()
    {
        settingsMenu.SetActive(false);
        player.SetActive(false);
        sceneRolling = true;
        gameStartScreen.SetActive(true);
    }

    public void openSettingsPause()
    {
        settingsMenuPause.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
