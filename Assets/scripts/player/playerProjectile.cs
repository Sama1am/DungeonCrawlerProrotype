using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float lifeTime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //damage = gameObject.GetComponent<playerManager>().damage;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 7);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        die();
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if(collision.gameObject.CompareTag("enemy"))
        {
            rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //collision.gameObject.GetComponent<enemyManager>().health -= damage;
            Destroy(gameObject);
        }


        if(collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }


    void die()
    {
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
