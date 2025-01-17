using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnObjectLow;  // Obstacle di tanah (kaktus)
    public GameObject[] spawnObjectHigh; // Obstacle terbang (burung)
    public GameObject[] spawnPoints; // Titik spawn (diatur untuk posisi spawn)
    public GameObject player;
    public Text DistanceUI;
    public Text highScoreUI;
    public static float score; // Jarak yang ditempuh oleh pemain
    public float highScore;
    public float randomMin = -2f; // Jarak acak untuk spawn objek
    public float randomMax = 2f; // Jarak acak untuk spawn objek
    public float timeBetweenSpawns = 5.0f; // Interval waktu spawn objek
    public float spawnSpeedIncrease = 0.5f; // Kecepatan spawn yang meningkat seiring waktu
    private float timer = 0f; // Timer untuk spawn objek
    private int spawnIndex = 0; // Untuk memilih titik spawn
    private float lastCheckedSpeed; // Kecepatan terakhir yang dicek

    void Start()
    {
        lastCheckedSpeed = parallax.speed;
        if(PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetFloat("highScore");
        }
        else
        {
            highScore = 0;
        }
    }

    void Update()
    {
        if (LogicScript.playerIsAlive)
        {
            timer += Time.deltaTime;  
            score += Time.deltaTime * 80f * parallax.speed * 10; 

            if (player == null)
            {
                highScore = PlayerPrefs.GetFloat("highScore");
            }

            // Save the high score
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat("highScore", highScore);
                PlayerPrefs.Save(); // Simpan perubahan
            }

            // Update UI untuk menunjukkan score (jarak)
            DistanceUI.text = "Score : " + score.ToString("F0");

            // High Score
            highScoreUI.text = "High Score : " + highScore.ToString("F0");
            
            // Cek perubahan kecepatan dan sesuaikan spawn
            if (timeBetweenSpawns > 2.0f)
            {
                AdjustTimeBetweenSpawns();
            }

            if (timeBetweenSpawns < 2.0f)
            {
                timeBetweenSpawns = 2f;
            }

            if (timer >= timeBetweenSpawns)
            {
                timer = 0f;
                SpawnObstacle();  // Panggil fungsi untuk spawn objek
            }
        }
    }

    void AdjustTimeBetweenSpawns()
    {
        // Cek jika perbedaan speed melebihi atau sama dengan 0.1
        if (Mathf.Abs(lastCheckedSpeed - parallax.speed) >= 0.1f)
        {
            // Sesuaikan timeBetweenSpawns agar semakin cepat saat kecepatan bertambah
            timeBetweenSpawns = Mathf.Max(1f, timeBetweenSpawns * spawnSpeedIncrease);

            // Perbarui lastCheckedSpeed dengan nilai kecepatan yang baru
            lastCheckedSpeed = parallax.speed;
        }
    }

    void SpawnObstacle()
    {
        // Pilih array spawn objek berdasarkan spawnIndex (Ground/High)
        GameObject[] spawnObjectArray = Random.Range(0, 2) == 0 ? spawnObjectLow : spawnObjectHigh;

        if (spawnObjectArray == spawnObjectHigh)
        {
            spawnIndex = 1;
        }
        else 
        {
            spawnIndex = 0;
        }

        // Pilih objek secara acak dari array yang dipilih
        GameObject spawnObject = spawnObjectArray[Random.Range(0, spawnObjectArray.Length)];

        // Tentukan posisi spawn dengan randomisasi posisi horizontal (X) dan posisi vertikal (Y) untuk objek yang terbang
        float randomX = Random.Range(randomMin, randomMax);
        Vector3 spawnPosition = new Vector3(
            spawnPoints[spawnIndex].transform.position.x + randomX,
            spawnPoints[spawnIndex].transform.position.y,
            spawnPoints[spawnIndex].transform.position.z
        );

        // Spawn objek
        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
}

