using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomChange : MonoBehaviour
{
    [SerializeField]
    private Transform targetCamPos;

    [SerializeField]
    private Transform targetPlayerPos;

    [SerializeField]
    Camera cam;

    [SerializeField]
    private bool isBossDoor;

    [SerializeField]
    private GameObject needKeyPopUp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Player")) && (isBossDoor == false))
        {
            //cam.transform.position = targetCamPos.position;
            cam.transform.position = new Vector3(targetCamPos.transform.position.x, targetCamPos.transform.position.y, -10f);
            //cam.transform.position = targetCamPos.position;
            collision.gameObject.transform.position = targetPlayerPos.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


        }

        if((isBossDoor) && (collision.gameObject.CompareTag("Player")) && (collision.gameObject.GetComponent<playerManager>().hasKey == true))
        {
            cam.transform.position = targetCamPos.position;
            collision.gameObject.transform.position = targetPlayerPos.position;
        }
        else if((isBossDoor) && (collision.gameObject.CompareTag("Player")) && (collision.gameObject.GetComponent<playerManager>().hasKey == false))
        {
            StartCoroutine("keyPopUp");
        }
    }

    IEnumerator keyPopUp()
    {
        needKeyPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        needKeyPopUp.SetActive(false);
    }
   
}
