using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint = 0;
    [SerializeField] private float speed = 5;
    private bool paused = false;
    [SerializeField]
    TextMeshProUGUI count;
    private Coroutine myCoroutine;
    int i = 15;

    //private CharacterController cc;
    private List<CharacterController> ccs;

    // Start is called before the first frame update
    void Start()
    {
       
        
        rb = GetComponent<Rigidbody>();
        ccs = new List<CharacterController>();

        //StartCoroutine(MoveTowardsWaypoint());
    }





    private void FixedUpdate()
    {
       
        if (!paused)
        {
            MovePlatform();
            if (transform.position == waypoints[currentWaypoint].position)
            {
                StartCoroutine(PauseForSeconds(1.0f));
                DetermineNextWaypoint();
            }
        }
    }

    IEnumerator PauseForSeconds(float delay)
    {
        paused = true;
        // wait for seconds
        yield return new WaitForSeconds(delay);
        paused = false;
    }

    void MovePlatform()
    {
        float step = speed * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, step);
        rb.MovePosition(newPos);
        foreach (CharacterController cc in ccs) { 
            if (cc)            {
                Debug.Log("moving cc");
                cc.Move(rb.velocity * Time.deltaTime);
            }
        }
    }

    void DetermineNextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("cc is set");
            CharacterController cc = other.GetComponent<CharacterController>();
            ccs.Add(cc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            for (int i=0; i<ccs.Count; i++)
            {
                if( ccs[i] == cc )
                {
                    ccs.RemoveAt(i);
                }
            }
            Debug.Log("cc is unset");
            //cc = null;
        }
    }

}
