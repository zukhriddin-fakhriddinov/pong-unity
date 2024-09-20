using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = true;  // Determines if this is Player A or B
    public bool isMultiplayer = false;  // Determines if it's multiplayer mode

    public GameObject circle; 

    private Rigidbody2D rb;
    private Vector2 playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
        isMultiplayer = !isMultiplayer;  // Toggle between multiplayer and single-player
        Debug.Log("Multiplayer mode: " + isMultiplayer);
        }

        if(isPlayerA){
            PaddleAController();  // Player A uses W and S keys
        }
        else {
            if (isMultiplayer) {
                PaddleBController();  // Player B uses Arrow keys in multiplayer
            } else {
                AIController();  // AI controls Player B in single-player
            }
        }
    }

    private void PaddleAController()
    {
        // Use "W" for up, "S" for down for Player A
        float moveA = Input.GetAxisRaw("Vertical");  // "W" and "S" keys mapped
        playerMovement = new Vector2(0, moveA);  // Only affects Player A
    }

    private void PaddleBController()
    {
        if(circle.transform.position.y > transform.position.y + 0.5f){
            playerMovement = new Vector2(0, 1);
        } else if (circle.transform.position.y < transform.position.y - 0.5f){
            playerMovement = new Vector2(0, -1);
        } else {
            playerMovement = new Vector2(0, 0);
        }
    }

    private void AIController()
    {
        // AI follows the ball in single-player mode
        if (circle.transform.position.y > transform.position.y + 0.5f){
            playerMovement = new Vector2(0, 1);
        } else if (circle.transform.position.y < transform.position.y - 0.5f){
            playerMovement = new Vector2(0, -1);
        } else {
            playerMovement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = playerMovement * speed;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float speed = 10f;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     public bool isPlayerA = false;
//     public GameObject circle;

//     private Rigidbody2D rb;
//     private Vector2 playerMovement;
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(isPlayerA){
//             PaddleAController();
//         }else{
//             PaddleBController();
//         }
//     }

//     private void PaddleBController(){
//         if(circle.transform.position.y > transform.position.y + 0.5f){
//             playerMovement = new Vector2(0, 1);
//         } else if (circle.transform.position.y < transform.position.y - 0.5f){
//             playerMovement = new Vector2(0, -1);
//         } else {
//             playerMovement = new Vector2(0, 0);
//         }
//     }

//     private void PaddleAController(){
//         playerMovement = new Vector2(0, Input.GetAxis("Vertical"));
//     }

//     private void FixedUpdate(){
//         rb.linearVelocity = playerMovement * speed;
//     }
// }
