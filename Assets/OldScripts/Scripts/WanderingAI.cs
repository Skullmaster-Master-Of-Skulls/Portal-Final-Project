using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { alive, dead }
public class WanderingAI : MonoBehaviour
{
    private float enemySpeed = 3.0f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;
    private EnemyStates state;
    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;
    private float baseSpeed = 0.25f;
    float difficultySpeedDelta = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.alive;
    }
    public void SetDifficulty(int newDifficulty) {
        Debug.Log("wai.SetDifficulty:" + newDifficulty);
        enemySpeed = baseSpeed + (newDifficulty * difficultySpeedDelta);
    }



    void OnDifficultyChanged(int difficulty)
    {
        //Debug.Log("WanderingAI.setDifficulty(" + newDifficulty + ")");
        SetDifficulty(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyStates.alive)
        {
            // Move Enemy 
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
            // generate Ray 
            Ray ray = new Ray(transform.position, transform.forward);

            // Spherecast and determine if Enemy needs to turn 
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {

                GameObject hitObject = hit.transform.gameObject;
                Debug.Log("hit object:" + hit.transform.gameObject.name);
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    // SPHERE CAST HIT SO NOW FIRE LASER!
                    //onDrawGismosSelected();
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        Debug.Log("Fire Laser!");
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;

                    }

                }
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);


                }
                
            }
            //onDrawGismosSelected();
        }
    }

    private void onDrawGismosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;

        Debug.DrawLine(transform.position, rangeTest);

        Gizmos.DrawWireSphere(rangeTest, sphereRadius);

    }
    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }
}

