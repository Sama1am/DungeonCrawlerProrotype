using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clusterProjectile : MonoBehaviour
{

    [SerializeField]
    private float radiusCluster;

    private float moveSpeedCluster;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float nextShotCluster;

    [SerializeField]
    private float timeBetweenShotsCluster;

    [SerializeField]
    private GameObject simpleEnemy;

    [SerializeField]
    private int numberOfProjectilesCluster;

    private Vector2 startPoint;

    public bool spawnEnemies, error;

    private GameObject player;

    public Transform target;

    private float angle, rawAngle, dirRight, dirUp;
    
    private int randomAngleRangeMin, randomAngleRangeMax, randomAngle;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        spawnEnemies = false;
        StartCoroutine("spawnDely");
    }



    private void FixedUpdate()
    {
        try
        {
            Vector3 heading = target.position - transform.position;
            dirRight = checkLeftRight(transform.forward, heading, transform.up);
            dirUp = checkUpDown(transform.up, heading);

            determineAngle();

            if (spawnEnemies)
                spawnSimpleEnemies();

            if (Time.time > nextShotCluster)
                clusterShoot(numberOfProjectilesCluster);
        }
        catch
        {
            error = true;
        }
    }

    //checks to see if the player is either to the left or right
    float checkLeftRight(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    //checks if the player is above the boss 
    float checkUpDown(Vector3 up, Vector3 targetDir)
    {
        
        float dir = Vector3.Dot(up, targetDir);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    void determineAngle()
    {
        //arctan gets an angle between the x and y pointd then we add 360 and mode it by 360 to give us a range between 0 and 360 no negative numbers 
        rawAngle = Mathf.Atan2(dirUp, dirRight) * Mathf.Rad2Deg;
        angle = (rawAngle + 360) % 360;

        if(angle == 45)
        {
            randomAngleRangeMin = 1;
            randomAngleRangeMax = 87;
        }
        else if(angle == 135)
        {
            randomAngleRangeMin = 270;
            randomAngleRangeMax = 358;
        }
        else if(angle == 225)
        {
            randomAngleRangeMin = 180;
            randomAngleRangeMax = 268;
        }
        else if(angle == 315)
        {
            randomAngleRangeMin = 90;
            randomAngleRangeMax = 178;
        }
    }
        


    void clusterShoot(int numOfProjectiles)
    {
       
        startPoint = gameObject.transform.position;
        

        determineAngle();
        
        for (int i = 0; i <= numOfProjectiles - 1; i++)
        {
            moveSpeedCluster = Random.Range(8, 12);
            randomAngle = Random.Range(randomAngleRangeMin, randomAngleRangeMax);
            float projectileDirXposition = startPoint.x + Mathf.Sin((randomAngle * Mathf.PI) / 180) * radiusCluster;
            float projectileDirYposition = startPoint.y + Mathf.Cos((randomAngle * Mathf.PI) / 180) * radiusCluster;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeedCluster;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            proj.transform.SetParent(gameObject.transform);
        }

         nextShotCluster = Time.time + timeBetweenShotsCluster;
    }


    void spawnSimpleEnemies()
    {
        
        float numOFEnemies = Random.Range(2, 4);

        for (int i = 0; i <= numOFEnemies - 1; i++)
        {
            float randX = Random.Range(-10.5f, 10.5f);
            float randY = Random.Range(-5.5f, 5.5f);

            Vector2 spawnPoint = new Vector2(randX, randY);

            if (spawnPoint == new Vector2(0, 0))
            {
                return;
            }

            Instantiate(simpleEnemy, spawnPoint, Quaternion.identity);
        }

        StartCoroutine("spawnDely");

    }

    private IEnumerator spawnDely()
    {
        spawnEnemies = false;
        yield return new WaitForSeconds(10f);
        spawnEnemies = true;

    }



}
