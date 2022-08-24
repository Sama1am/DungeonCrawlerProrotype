using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    public float currentHealth;

    [SerializeField]
    private float health;

    [SerializeField]
    public float damage;

    public SpriteRenderer sp;
    GameObject target;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
       

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //flipGFX();
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine("changeColour");

        if (currentHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }



    private IEnumerator changeColour()
    {
        sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sp.color = Color.white;
    }




}
