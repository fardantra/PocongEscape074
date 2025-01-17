using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseButton;
    
    void Awake()
    {
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.isPaused && LogicScript.playerIsAlive)
        {
            pauseButton.SetActive(true);
        }

        else 
        {
            pauseButton.SetActive(false);
        }
    }
}
