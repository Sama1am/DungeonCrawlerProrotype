using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool canMove;

    [SerializeField]
    private float slowRange;

    [SerializeField]
    private bool closeToTarget;

    private float ogSpeed;

    private Transform target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        ogSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        if(canMove)
        {
            moveToTarget();
            //isCloseToTarget();
        }
        
    }


    private void moveToTarget()
    {
        Vector3 posA = target.position;
        Vector3 posB = rb.position;
        Vector3 direction = (posA - posB).normalized;


        Vector2 force = direction * speed * Time.deltaTime;
        rb.velocity = rb.velocity + force;
    }

    private void isCloseToTarget()
    {
        if((Vector2.Distance(transform.position, target.position)) <= (slowRange))
        {
            speed -= 2;
        }
        else
        {
            speed = ogSpeed;
        }
    }


    private void OnDrawGizmos()
    {
        if(target == null)
        {
            return;
        }


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);

    }
}
