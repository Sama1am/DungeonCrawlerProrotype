using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjLogic : MonoBehaviour
{

    [SerializeField]
    private float lifeTime;

    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = gameObject.GetComponentInParent<playerManager>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        
        if(collision.gameObject.CompareTag("boss"))
        {
            collision.gameObject.GetComponent<bossManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        
        
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
