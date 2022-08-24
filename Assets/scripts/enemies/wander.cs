using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class wander : MonoBehaviour
{
    public Transform target;

    public float speed;
    public float nextWayPointDist, knockBackForce;

    public bool canMove;
    public Path path;
    int currentWayPoint = 0;
    public bool reachedEndOfPath, foundPath = false;
    private float distanceToWaypoint;

    private float damage;

    Seeker seeker;
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Start()
    {
        seeker = gameObject.GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>()[0];
        canMove = false;
        selectNewPath();
        StartCoroutine("startDelay");

        damage = GetComponent<EnemyManager>().damage;
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count) // cheks to see if we have reached end of path if so then we stop 
        {
            reachedEndOfPath = true;
            StartCoroutine("moveWait");
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);

        if(canMove)
        {
            move();
        }
        

        
    }


    void onPathComplete(Path p)
    {
        if(!p.error)
        {
            reachedEndOfPath = false;
            path = p; //setting our current path to the new genereated path
            currentWayPoint = 0; //reset our progress along the path, so we can start at the beginning of our new path 
            Debug.Log("HAVE NEW PATH!");
        }

        
    }


    void selectNewPath()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker.StartPath(rb.position, target.position, onPathComplete);
        reachedEndOfPath = false;
    }

    void move()
    {

        //slows down the character as they get close to target 
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWayPointDist) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;

        transform.position += velocity * Time.deltaTime;

        if ((Vector2.Distance(rb.position, path.vectorPath[currentWayPoint])) < (nextWayPointDist))
        {
            currentWayPoint++;
        }

        if(dir.x < 0)
        {
            sr.flipX = true;
        }
        else if(dir.x > 0)
        {
            sr.flipX = false;
        }
    }

    public void knockBackPlayer()
    {
        rb.AddForce(-target.position * knockBackForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("COLLIDED WITH AN ENEMY!");
            //knockBackPlayer();

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLIDED WITH THE PLAYER!");
            target.GetComponent<playerManager>().takeDamage(damage);
            knockBackPlayer();

        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            knockBackPlayer();
        }
    }


    private IEnumerator moveWait()
    {
        
        yield return new WaitForSeconds(1f);
        selectNewPath();
        
    }

    private IEnumerator startDelay()
    {

        yield return new WaitForSeconds(0.5f);
        canMove = true;

    }

}
