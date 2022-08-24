using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{


    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void healthPowerUp()
    {
        player.GetComponent<playerManager>().currentHealth += 2;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<playerManager>().currentHealth == collision.gameObject.GetComponent<playerManager>().health)
            {
                return;
            }

            healthPowerUp();
            Destroy(gameObject);
        }
    }
}
