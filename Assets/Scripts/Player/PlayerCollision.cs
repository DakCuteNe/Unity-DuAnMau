using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Enemy"))
        {
            float playerY = transform.position.y;
            float enemyY = collision.transform.position.y;
            float offset = 0.8f;

            if (playerY > enemyY + offset)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
            else
            {
                gameManager.GameOver();
            }
        }
    }
}
