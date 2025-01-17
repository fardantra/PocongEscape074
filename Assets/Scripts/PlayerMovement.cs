using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // Jump force
    public float slideSpeed = 5f; // Speed during slide
    public float slideCooldown = 1f; // Cooldown after sliding

    public Animator animator;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private bool isDead = false;
    private bool isGrounded;
    private bool isSliding = false; // Sliding state
    private int jumpCount = 0;
    private float cooldownTime = 0f;
    AudioManager audioManager;
    PlayerMovement playerMovement;
    private Vector2 initialPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerCollider = GetComponent<BoxCollider2D>();
        isGrounded = true;

        // Menyimpan posisi awal player
        initialPosition = transform.position;

        if (playerMovement != null)
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Debug.Log(jumpCount);
        // Update animator states
        animator.SetBool("isSliding", isSliding);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetInteger("jumpCount", jumpCount);
        animator.SetBool ("isDead", isDead);

        if (!LogicScript.playerIsAlive)
        {
        // Disable the animator when the player is not alive
        playerCollider.size = new Vector2(6.196233f, 14.81557f);
        
        // Enable the dead animation.
        animator.SetBool("isDead", true);
        rb.linearVelocity = Vector2.zero; // Reset vertical and horizontal velocity to ensure the player stops moving 
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze player position after dead
        
        return; // Skip the rest of the logic

        }

        if (LogicScript.playerIsAlive)
        {
            isDead = false;
        }

        // Enable the animator if the player is alive
        animator.enabled = true;

        // Jump logic (double jump)
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount <= 2) && LogicScript.playerIsAlive)
        {
            Jump();
        }

        // Start sliding only when grounded, and the cooldown is over
        if (isGrounded && cooldownTime <= 0 && Input.GetButtonDown("Slide"))
        {
            StartSliding();
        }

        // Continue sliding as long as the "Slide" button is held
        if (isSliding && Input.GetButton("Slide"))
        {
            ContinueSliding();
        }

        // Slide cooldown
        if (isSliding && !Input.GetButton("Slide"))
        {
            EndSliding();
        }
        else
        {
            cooldownTime -= Time.deltaTime;
        }

        // Vertical movement (jumping, sliding)
        if (!isSliding)
        {
            float horizontalInput = 0; // Move player horizontally (not affected during sliding)
            MovePlayer(horizontalInput);
            
        }
        
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity to prevent stacking jumps
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerCollider.size = new Vector2(3, 7f);
        jumpCount++;
        audioManager.JumpAudio(audioManager.jumpSound);
    }

   private void StartSliding()
{   
    isSliding = true;
    cooldownTime = slideCooldown; // Set cooldown time after sliding
    transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f); // Lower the player
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -0.5f); // Drop the player quickly when starting the slide
    playerCollider.size = new Vector2(13.63003f, 8.827233f);
    audioManager.PlaySlideSound(audioManager.slideSound);  // Play the slide sound
}

    private void ContinueSliding()
    {
        // During the slide, continue to move the player at the defined slide speed
        rb.linearVelocity = new Vector2(slideSpeed * Mathf.Sign(rb.linearVelocity.x), rb.linearVelocity.y);
        playerCollider.size = new Vector2(13.63003f, 8.827233f);
    }
    

    private void EndSliding()
    {
        isSliding = false;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f); // Raise the player
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y); // Reset vertical velocity
        playerCollider.size = new Vector2(6.196233f, 14.81557f);
        audioManager.StopSlideSound();  // Stop the slide sound
        cooldownTime = slideCooldown; // Reset cooldown after sliding ends
    }

public void ResetPlayerState()
{
    // Reset Animator
    animator.Rebind(); 
    animator.Update(0); 
    animator.SetBool("isSliding", false);
    animator.SetBool("isGrounded", true);
    animator.SetInteger("jumpCount", 0);
    animator.SetBool("isDead", false);

    // Reset State
    isSliding = false;
    isGrounded = true;
    jumpCount = 0;

    // Reset posisi player ke posisi awal
    transform.position = initialPosition;

    // Reset Rigidbody2D untuk menghilangkan pengaruh dari fisika
    rb.linearVelocity = Vector2.zero;          
    rb.angularVelocity = 0f;              

    // Reset Collider jika diperlukan
    playerCollider.size = new Vector2(6.196233f, 14.81557f); // Atur ukuran collider sesuai keperluan
}

    // Player movement (fixed horizontal, only vertical motion)
    private void MovePlayer(float horizontalInput)
    {
        if (!isSliding)
        {
            // Normal movement logic (not during sliding)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }

    // Check if the player is on the ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when touching the ground
            playerCollider.size = new Vector2(6.196233f, 14.81557f);
        }
    }

    // Reset grounded status when leaving the ground
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    
}