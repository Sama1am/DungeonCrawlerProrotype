using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderMovement : MonoBehaviour
{

    public Vector3 targetPos;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    public bool canMove, atTargetPos;

    [SerializeField]
    public float speed;

    [SerializeField]
    private float closeEnough;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("moveBuildUp");
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            move();
        }

        if((gameObject.transform.position == targetPos)) //|| (Vector2.Distance(transform.position, targetPos)) <= (closeEnough))
        {
            canMove = false;

            atTargetPos = true;
            
        }

        if(atTargetPos)
        {
            StartCoroutine("moveBuildUp");
        }
    }


    private void move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        Debug.Log("BOSS SHOULD MOVE");
    }

    public void selectTarget()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }

    public void knockBack()
    {
        rb.AddForce(-targetPos * knockBackForce, ForceMode2D.Impulse);
    }

    private IEnumerator moveBuildUp()
    {
        canMove = false;
        atTargetPos = false;

        yield return new WaitForSeconds(3f);
        selectTarget();

        atTargetPos = false;
        canMove = true;

    }

    
}
