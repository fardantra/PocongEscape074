using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    PlayerMovement playerMovement;
    public static bool isPaused;
    public GameObject settingsMenuPause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (playerMovement != null)
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        
    }
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        settingsMenuPause.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false); // Sembunyikan Pause Menu
        settingsMenuPause.SetActive(true); // Tampilkan Settings Menu
    }

    public void CloseSettings()
    {
        settingsMenuPause.SetActive(false); // Sembunyikan Settings Menu
        pauseMenu.SetActive(true); // Tampilkan Pause Menu kembali
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LogicScript.playerIsAlive = false;
        LogicScript.sceneRolling = true;
        parallax.speed = 0.2f;
        GameManager.score = 0;
        PauseMenuScript.isPaused = false;
        Time.timeScale = 1f;
    } 

    public void restartGame() // restart the game
    {
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        GameManager.score = 0;
        LogicScript.playerIsAlive = true;
        LogicScript.sceneRolling = true;
        parallax.speed = 0.2f;
        PauseMenuScript.isPaused = false;
        parallax.distance = 0f;
        Time.timeScale = 1f;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetPlayerState();
        }
        destroyObjects();
    }

    public void quitGame()
    {
        Application.Quit();
    }



    private void destroyObjects ()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] money = GameObject.FindGameObjectsWithTag("Uang"); 
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);

        }

        foreach (GameObject uang in money)
        {
            Destroy(uang);
        }
    }
}
