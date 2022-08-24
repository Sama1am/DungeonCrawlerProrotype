using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementTopDown : MonoBehaviour
{

    #region Movement
    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float dragForce;
    [SerializeField] private float startBoost;
    private float horizontalPos;
    //checks to see if we are turning or have turned 
    private bool changeDir => (rb.velocity.x > 0f && horizontalPos < 0f) || (rb.velocity.x < 0f && horizontalPos > 0f);
    Vector2 _move;
    #endregion

    #region Player GFX
    [Header("player GFX")]
    [SerializeField] private SpriteRenderer sr;
    #endregion

    #region Shoot
    //[SerializeField]
    //private float shootSpeed;

    //[SerializeField]
    //private bool canShoot;

    //[SerializeField]
    //private float shootDelay;

    //[SerializeField]
    //private GameObject LRSpawnPoint;

    //[SerializeField]
    //private GameObject topSpawnPoint;

    //[SerializeField]
    //private GameObject bottomSpawnPoint;

    //[SerializeField]
    //private GameObject bullet;


    #endregion


    Rigidbody2D rb;

    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); 
        vertical = Input.GetAxisRaw("Vertical");

        flipPlayer();
        //shoot();
    }

    private void FixedUpdate()
    {
        
        if(horizontal != 0 || vertical != 0)
        {
            movement();
        }

        horizontalDrag();

    }


    public void movement()
    {
        float xpos;
        xpos = Input.GetAxisRaw("Horizontal");


        _move = new Vector2(horizontal, vertical);
        _move *= (moveAcceleration);

        //to make beinging moevment snappy 
        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(_move * startBoost);
        }
        else
        {
            rb.AddForce(_move);
        }

        //checks to see what velcoity the character is at, if it is bigger or equal to max move speed it then clamps it to the max move speed 

        if (Mathf.Abs(rb.velocity.x) >= maxMoveSpeed)
        {
            //Debug.Log("AT MAX MOVE SPEED");
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
            //Debug.Log("VELOCITY IS " + rb.velocity.x);
        }

    }



    public void horizontalDrag()
    {
        if ((horizontalPos == 0) || (changeDir))   //change xpos to horizontalPos
        {
            rb.drag = dragForce;
        }
        else if ((horizontalPos != 0)) //change xpos to horizontalPos   || (Mathf.Abs(rb.velocity.y) > 5)
        {
            //so if it is jumping it sets the drag force to zero
            rb.drag = dragForce;

        }
    }


    private void flipPlayer()
    {
       

        if (rb.velocity.x > 0f)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            sr.flipX = true;
        }

    }


   
}
