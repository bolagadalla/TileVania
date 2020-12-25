using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Variables
    [SerializeField] float speed = 1f;

    //caching Components
    Rigidbody2D enemyRigidbody2D;
    BoxCollider2D flipBox;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        flipBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            enemyRigidbody2D.velocity = new Vector2(speed, 0f);
        }
        else
        {
            enemyRigidbody2D.velocity = new Vector2(-speed, 0f);
        }
    }

    /// <summary>
    /// Returning true if the sprite local scale on the x is greater then 0, Which means its moving to the right
    /// </summary>
    /// <returns></returns>
    private bool IsFacingRight()
    {
        //Returning true if the local scale is greater then 0 aka 1. Other wise its false meaning its -1 which means its moving to the left
        return transform.localScale.x > 0;
    }

    // Flipping the enemy character
    void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody2D.velocity.x)), 1);
    }
}
