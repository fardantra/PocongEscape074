using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Komponen VideoPlayer
    public string gameplaySceneName = "SampleScene"; // Nama scene gameplay

    private bool isVideoPlaying = true;

    void Start()
    {
        // Pastikan video mulai diputar
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndVideo; // Event ketika video selesai
        }
    }

    void Update()
    {
        // Deteksi input untuk skip cutscene
        if (isVideoPlaying && Input.anyKeyDown)
        {
            SkipCutscene();
        }
    }

    private void SkipCutscene()
    {
        isVideoPlaying = false;

        // Hentikan video jika masih berjalan
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        // Pindah ke scene gameplay
        SceneManager.LoadScene(gameplaySceneName);
    }

    private void EndVideo(VideoPlayer vp)
    {
        // Pindah ke scene gameplay ketika video selesai
        if (isVideoPlaying)
        {
            isVideoPlaying = false;
            SceneManager.LoadScene(gameplaySceneName);
        }
    }
}
