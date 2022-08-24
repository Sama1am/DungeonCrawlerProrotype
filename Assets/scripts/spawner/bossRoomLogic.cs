using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossRoomLogic : MonoBehaviour
{

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private GameObject EnemHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            EnemHealthUI.SetActive(true);
        }
    }
}
