using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbingSpeed = 3f;
    [SerializeField] float playerGravity;

    // State
    [SerializeField] bool isAlive = true;

    // Cached Components
    Rigidbody2D rig2d;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;
    [SerializeField] GameObject deathPanel;


    // Start is called before the first frame update
    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();

        playerGravity = rig2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        // if the boolean isalive is false then it would return and would not do the code under the if statement, other wise it will allow the other methods underneath to be active.
        if (!isAlive){return;}
        Die();
        Run();
        ClimbLadder();
        Jump();
        FlipPlayer();
    }

    #region Player Methods

    /// <summary>
    /// Run method by using a horizontal velocity and playing the running animation at the same time
    /// </summary>
    private void Run()
    {
        // this float will return a value of - or + indicating the direction
        float moveHor = Input.GetAxis("Horizontal");
        // calculating a player velocity as a new vector 2
        Vector2 playerVelocity = new Vector2(moveHor * runSpeed, rig2d.velocity.y);
        // Adding the calculated playervelocity vector 2 as a velocity to the rigidbody 
        rig2d.velocity = playerVelocity;

        // True if the player has a velocity on the x axises greater then a small value of less then 1
        bool playerHasHorizontalSpeed = Mathf.Abs(rig2d.velocity.x) > Mathf.Epsilon;
        //turning on the animation condition
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    /// <summary>
    /// Jump Method that will add a vertical velocity upward while touching the ground layer, this way the player can only jump once
    /// </summary>
    private void Jump()
    {
        // if the feet collider is NOT touching the layer ground then do nothing, the character cant move further up
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        // if you press the jump button which is spacebar then you add a jump velocity upward as a vector 2 and adding it to the rigidbody velocity
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rig2d.velocity += jumpVelocityToAdd;
            //myAnimator.SetBool("Jumping", true);
        }        
    }

    /// <summary>
    /// Climbing the ladder by using vertical velocity and playing the climbing animation
    /// </summary>
    private void ClimbLadder()
    {
        // if the feet collider IS touching the ladder layer then you can use the vertical keys to climb, by simply adding a velocity upward to the rigidbody
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            float climb = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(rig2d.velocity.x, climb * climbingSpeed);

            rig2d.velocity = playerVelocity;

            // while the character is still touching the ladder layer the gravity will be 0 to simulate standing on the ladder, rather then flotting downward
            rig2d.gravityScale = 0f;

            // if the player has a vertical speed then use the climb animation trigger
            bool playerHasVerticalSpeed = Mathf.Abs(rig2d.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
        else // if none of those condition are meet, then just make the gravity normal and the climbing animation bool trigger to be false and finally return and do nothing else
        {
            rig2d.gravityScale = playerGravity;
            myAnimator.SetBool("Climbing", false);
            return;
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isAlive = false;
            rig2d.velocity = new Vector2(10f, 20f);
            myAnimator.SetTrigger("Dying");
            deathPanel.SetActive(true);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    /// <summary>
    /// Flips the player according to the direction of their horizontal speed
    /// </summary>
    private void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rig2d.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rig2d.velocity.x), 1);
        }
    }



    #endregion
}
