using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public GameObject fullButton;
    public GameObject muteButton;
    
    // Update is called once per frame
    void Update()
    {
        if (AudioListener.volume == 0)
        {
            fullButton.SetActive (false);
            muteButton.SetActive (true);
        }
        else 
        {
            fullButton.SetActive (true);
            muteButton.SetActive (false);
        }
    }
}
