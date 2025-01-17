using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    [Range(0f, 0.5f)]
    private Rigidbody2D rb;
    public LogicScript logic;
    public Animator animator;

    [Range(1f, 2f)]
    public float individualSpeed = 1f; // Change individual speed of the sprite

    private bool hasAnimationClip = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // check if prefab has animation clip
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            hasAnimationClip = animator.runtimeAnimatorController.animationClips.Length > 0;
        }
        else
        {
            hasAnimationClip = false; // Tidak ada Animation Clip
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }

        if (LogicScript.playerIsAlive)
        {
            rb.linearVelocity =  Vector2.left * parallax.speed * 20f * individualSpeed; //Multiplied by 20 to match the background speed.
        }
        
        if (!LogicScript.playerIsAlive)
        {
            rb.linearVelocity =  Vector2.left * 0; //Stop the box.
        }

        if (!LogicScript.playerIsAlive && hasAnimationClip)
        {
            animator.enabled = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision){
        logic.gameOver();
    }
}
