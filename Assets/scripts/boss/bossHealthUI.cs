using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthUI : MonoBehaviour
{

    //GameObject boss;

    [SerializeField]
    private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        //boss = GameObject.FindGameObjectWithTag("boss");

        gameObject.GetComponent<Slider>().maxValue = boss.GetComponent<bossManager>().health;
        gameObject.GetComponent<Slider>().minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            setUI();
        }
        catch
        {
            Debug.Log("ISSUE WITH BOSS HEALTH SLIDER1");
        }
       
    }

    void setUI()
    {
        gameObject.GetComponent<Slider>().value = boss.GetComponent<bossManager>().currentHealth;
    }
}
