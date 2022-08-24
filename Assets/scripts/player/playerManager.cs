using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour
{
    [SerializeField]
    public float health;

    [SerializeField]
    public float currentHealth;

    [SerializeField]
    private GameObject GFX;

    [SerializeField]
    private float nextShot;

    [SerializeField]
    private float timeBetweenShots;

    public bool hasKey;

    [SerializeField]
    private GameObject playerProj;

    public float damage;

    [SerializeField]
    private float force;

    public AudioClip[] clips;

    public AudioSource[] audioSource;
    Color OG;

    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        OG = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        hasKey = false;
        currentHealth = health;

        audioSource[0].clip = clips[0];
        audioSource[0].Play();
        audioSource[0].loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();

        if(currentHealth > health)
        {
            currentHealth = health;
        }
    }

    public void takeDamage(float damage)
    {
        audioSource[1].loop = false;
        audioSource[1].clip = clips[2];
        audioSource[1].Play();

        currentHealth -= damage;
        StartCoroutine("changeColour");

        if (currentHealth <= 0)
        {
            audioSource[1].loop = false;
            audioSource[1].clip = clips[1];
            audioSource[1].Play();
            die();
        }
    }

    private void die()
    {
        
        SceneManager.LoadScene(2);
        
    }

    private void shoot()
    {
        if((Input.GetKeyDown(KeyCode.UpArrow)) && (Time.time > nextShot))
        {
            var proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
            proj.transform.SetParent(gameObject.transform);
            proj.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
            nextShot = Time.time + timeBetweenShots;

        }
        
        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (Time.time > nextShot))
        {
            var proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
            proj.transform.SetParent(gameObject.transform);
            proj.GetComponent<Rigidbody2D>().AddForce(-transform.up * force, ForceMode2D.Impulse);
            nextShot = Time.time + timeBetweenShots;
        }
        
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (Time.time > nextShot))
        {
            var proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
            proj.transform.SetParent(gameObject.transform);
            proj.GetComponent<Rigidbody2D>().AddForce(-transform.right * force, ForceMode2D.Impulse);
            nextShot = Time.time + timeBetweenShots;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (Time.time > nextShot))
        {
            var proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
            proj.transform.SetParent(gameObject.transform);
            proj.GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
            nextShot = Time.time + timeBetweenShots;
        }
    }


    private IEnumerator changeColour()
    {
        sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sp.color = OG;
    }
}



