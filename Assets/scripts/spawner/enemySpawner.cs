using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject room;

    [SerializeField]
    private GameObject health;

    [SerializeField]
    private List<GameObject> spawnedEnemies;

    [SerializeField]
    private GameObject centerRoom;

    [SerializeField]
    private bool easy, medium, hard, random, isBoss, isPlayerInRoom, hasSpawnedEnemies, active, spawnedHealth;

    [SerializeField]
    private int minRange, MaxRange;

    [SerializeField]
    private int maxX, minX, maxY, minY;

    private int numOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnedHealth = false;
        hasSpawnedEnemies = false;
        active = false;
        isPlayerInRoom = false;
        spawnedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if ((isPlayerInRoom == true) && (hasSpawnedEnemies == false))
        {
            
            if(easy)
            {
                easyRoom();
            }
            else if(medium)
            {
                mediumRoom();
            }
            else if(hard)
            {
                hardRoom();
            }
            else if(random)
            {
                randomRoom();
            }
        }

        for (var i = spawnedEnemies.Count - 1; i > -1; i--)
        {
            if (spawnedEnemies[i] == null)
                spawnedEnemies.RemoveAt(i);
        }

        if ((hard) && (hasSpawnedEnemies == true))
        {
            if ((spawnedEnemies.Count == 0) && (spawnedHealth == false))
            {
                Instantiate(health, new Vector3(centerRoom.transform.position.x, centerRoom.transform.position.y, 0), centerRoom.transform.rotation);
                spawnedHealth = true;
            }
        }
    }

    void easyRoom()
    {
        numOfEnemies = Random.Range(minRange, MaxRange);

        for (int i = 0; i <= numOfEnemies - 1; i++)
        {
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);
            Vector2 spawnPoint = new Vector2(randomX, randomY);

            GameObject enemy = Instantiate(enemies[0], spawnPoint, Quaternion.identity);

            enemy.transform.SetParent(room.transform);
            
            spawnedEnemies.Add(enemy);

        }

        hasSpawnedEnemies = true;
        active = true;
    }


    void mediumRoom()
    {
        numOfEnemies = Random.Range(minRange, MaxRange);

        for (int i = 0; i <= numOfEnemies - 1; i++)
        {
            int whatEnemy = Random.Range(0, 2);
            Debug.Log(whatEnemy + "enemy spawned!");
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);
            Vector2 spawnPoint = new Vector2(randomX, randomY);

            GameObject enemy = Instantiate(enemies[whatEnemy], spawnPoint, Quaternion.identity);
            enemy.transform.SetParent(room.transform);
            spawnedEnemies.Add(enemy);
        }

        hasSpawnedEnemies = true;
        active = true;
    }

    void hardRoom()
    {
        numOfEnemies = Random.Range(minRange, MaxRange);

        for (int i = 0; i <= numOfEnemies - 1; i++)
        {
            int whatEnemy = Random.Range(0, 3);
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);
            Vector2 spawnPoint = new Vector2(randomX, randomY);

            GameObject enemy = Instantiate(enemies[whatEnemy], spawnPoint, Quaternion.identity);
            enemy.transform.SetParent(room.transform);
            spawnedEnemies.Add(enemy);
        }

        hasSpawnedEnemies = true;
        active = true;
    }

    void randomRoom()
    {
        numOfEnemies = Random.Range(minRange, MaxRange);

        for (int i = 0; i <= numOfEnemies - 1; i++)
        {
            int whatEnemy = Random.Range(0, 3);
            int randomX = Random.Range(minX, maxX);
            int randomY = Random.Range(minY, maxY);
            Vector2 spawnPoint = new Vector2(randomX, randomY);

            GameObject enemy = Instantiate(enemies[whatEnemy], spawnPoint, Quaternion.identity);
            enemy.transform.SetParent(room.transform);
            spawnedEnemies.Add(enemy);
        }

        hasSpawnedEnemies = true;
        active = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRoom = true;

            if(hasSpawnedEnemies)
            {
                //doing this for a reason idk idk idk
                for (int i = 0; i <= spawnedEnemies.Count - 1; i++)
                {
                    spawnedEnemies[i].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRoom = false;
            active = false;

            for (int i = 0; i <= spawnedEnemies.Count - 1; i++)
            {
                spawnedEnemies[i].SetActive(false);
            }
        }
          
    }



}
