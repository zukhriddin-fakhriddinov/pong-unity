using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float speedIncrease = 0.2f;
    public Text playerText;
    public Text opponentText;
    private int hitCounter;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate(){
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity,initialSpeed + (speedIncrease*hitCounter));
    }

    private void StartBall(){
        rb.linearVelocity=new Vector2(-1, 0)*(initialSpeed + speedIncrease* hitCounter);
    }

    private void RestartBall(){
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject){
        hitCounter++;
        Debug.Log("Hit Counter: " + hitCounter);

        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = myObject.position;

        float xDirection = (transform.position.x > 0) ? -1 : 1;
        float yDirection = (ballPosition.y - playerPosition.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0){
            yDirection = 0.25f;
        }

        rb.linearVelocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }
    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight"){
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (transform.position.x > 0){
            RestartBall();
            playerText.text = (int.Parse(playerText.text) + 1).ToString();
        }
        else if (transform.position.x < 0){
            RestartBall();
            opponentText.text = (int.Parse(opponentText.text) + 1).ToString();
        }
    }
}
