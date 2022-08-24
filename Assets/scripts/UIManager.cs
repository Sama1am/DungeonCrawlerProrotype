using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] healthImages;

    [SerializeField]
    private GameObject[] uiHealth;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        health();
    }

    void health()
    {
        if(player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[0];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 1)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[1];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 2)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[2];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 3)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[1];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[2];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 4)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[0];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[2];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[2];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 5)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[1];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[2];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[2];
        }

        if (player.GetComponent<playerManager>().currentHealth == player.GetComponent<playerManager>().health - 6)
        {
            uiHealth[0].GetComponent<Image>().sprite = healthImages[2];
            uiHealth[1].GetComponent<Image>().sprite = healthImages[2];
            uiHealth[2].GetComponent<Image>().sprite = healthImages[2];
        }
    }
}
