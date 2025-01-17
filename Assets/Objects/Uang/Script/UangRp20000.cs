using UnityEngine;

public class UangRp20000 : MonoBehaviour
{
    public float moveSpeed = 5f;
    AudioManager audioManager;
    
    void Awake (){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        // Jika player sudah mati, hancurkan koin
        if (!LogicScript.playerIsAlive)
        {
            Destroy(gameObject);
            return;
        }
        
        // Koin bergerak ke kiri seiring waktu
        if (LogicScript.playerIsAlive){
            transform.Translate(Vector2.left * parallax.speed * 23f * Time.deltaTime);
        }
        // Hapus koin jika sudah di luar layar
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.moneySound);
            // Tambahkan nilai koin ke total score
            UangManager uangManager = FindAnyObjectByType<UangManager>();
            if (uangManager != null)
            {
                uangManager.AddScore(20000);
            }
            
            // Hancurkan koin saat pemain menyentuhnya
            Destroy(gameObject);
        }
    }
}
