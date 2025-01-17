using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class parallax : MonoBehaviour
{
    Material mat;
    public static float distance;

    private PlayerMovement playerMovement;

    [Range(0f, 0.8f)]
    public static float speed = 0.2f;
    private float lastScoreCheckpoint = 0; 
    private float targetSpeed = 0.2f;      
    public float speedIncreaseRate = 0.05f; 
    

    void Awake(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }
    void Start(){
        mat = GetComponent<Renderer>().material;
    }

    void Update(){
        if (LogicScript.sceneRolling)
        {
            distance+= Time.deltaTime*speed; // moving the background
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }
        // Periksa jika score telah bertambah dari checkpoint terakhir
        if (GameManager.score >= lastScoreCheckpoint + 5000 && speed < 0.7f)
        {
            // Tingkatkan speed sebesar 0.1f
            targetSpeed += 0.1f;

            // Perbarui checkpoint score
            lastScoreCheckpoint += 5000;
        }
        speed = Mathf.MoveTowards(speed, targetSpeed, speedIncreaseRate * Time.deltaTime);
    }
}
