using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMoveAI : MonoBehaviour
{
    public bool canMove;
    private Transform target;

    [SerializeField]
    public float speed;

    [SerializeField]
    private float ogSpeed;

    [SerializeField]
    private float nextWayPointDist;

    [SerializeField]
    private float increaseSpeedRange;

    [SerializeField]
    private float speedIncrease;

    [SerializeField]
    private float edgeForce;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEnd = false;


    Rigidbody2D rb;
    Seeker seeker;
    GameObject player;
    public SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //move();
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count) // checking to see if we have reached end of path 
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        if((Vector2.Distance(transform.position, player.GetComponent<Transform>().position)) <= increaseSpeedRange)
        {
            speed += speedIncrease;
        }
        else if((Vector2.Distance(transform.position, player.GetComponent<Transform>().position)) >= increaseSpeedRange)
        {
            speed = ogSpeed;
        }

        if(canMove)
        {
            move();
        }
        
    }


    void move()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.velocity = rb.velocity + force;


        //rb.AddForce(force);

        //transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);

        //float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);



        if ((Vector2.Distance(rb.position, path.vectorPath[currentWayPoint])) < (nextWayPointDist))
        {
            currentWayPoint++; 
        }
    }

    void onPathDone(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void updatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position + new Vector3(0.5f, 00.5f, 0f), onPathDone);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            rb.AddForce(-target.position * edgeForce, ForceMode2D.Impulse);
        }

        //if(collision.gameObject.CompareTag("playerProj"))
        //{
        //    StartCoroutine("changeColour");
        //}
    }

    private IEnumerator changeColour()
    {
        sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sp.color = Color.white;
    }

}
