using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip BGM;
    public AudioClip slideSound;
    public AudioClip jumpSound;
    public AudioClip catMeowSound;
    public AudioClip gameOverSound;
    public AudioClip moneySound;
    bool SoundPlayed = false;

    void Awake()
    {
        // Play background music
        musicSource.clip = BGM;
        musicSource.loop = true;
        musicSource.Play();
        
        SoundPlayed = false;
        
        // Terapkan volume dari PlayerPrefs
        musicSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        SFXSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    // Method to play one-shot sound effects
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Method to play the slide sound, with looping enabled
    public void PlaySlideSound(AudioClip clip)
    {
        // If the clip is already playing, reset it before playing again
        if (SFXSource.isPlaying && SFXSource.clip == clip)
        {
            SFXSource.Stop(); // Stop the current playing sound
        }

        // Set the slide sound clip and enable looping
        SFXSource.clip = clip;
        SFXSource.loop = true;  // Enable looping
        SFXSource.Play();      // Start the slide sound (loops indefinitely)
    }

    public void JumpAudio(AudioClip clip)
    {
        if (LogicScript.playerIsAlive == true)
        {
            PlaySFX(jumpSound);
        }
    }

    // Method to stop the slide sound when sliding ends
    public void StopSlideSound()
    {
        if (SFXSource.isPlaying && SFXSource.loop)
        {
            SFXSource.Stop();  // Stop the looped slide sound
        }
    }

    // Play game over music
    public void GameOverAudio()
    {   
        if (!SoundPlayed)
        {
            musicSource.loop = false;
            musicSource.clip = gameOverSound;
            musicSource.Play();
            SoundPlayed = true;
        }
    }

    // Optionally: You could also provide a method to stop any other sound effects
    public void StopAllSFX()
    {
        if (SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }
    }

    public void MuteFullVolume()
    {
        // Jika volume saat ini tidak mute, set ke 0. Jika sudah mute, set kembali ke 1.
        AudioListener.volume = (AudioListener.volume > 0) ? 0 : 1;
    }
    
    public void StopThenPlaySFX(AudioClip clip)
    {
        if (SFXSource.isPlaying) 
        {
            SFXSource.Stop(); // Stop the current sound if any
        }

        SFXSource.PlayOneShot(clip);  // Play the new sound
    }

    public void SetBGMVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // Simpan ke PlayerPrefs
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // Simpan ke PlayerPrefs
        PlayerPrefs.Save();
    }
}

