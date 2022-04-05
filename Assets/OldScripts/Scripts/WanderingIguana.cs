using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingIguana : MonoBehaviour
{

    private float iguanaSpeed = 3.0f;
    private float obstacleRange = 9.0f;

    private Animator anim;

    private float turn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = iguanaSpeed;
    }

    // Update is called once per frame

    private void Move(float turn, float forward)
    {
        float dampTime = 0.2f;
        if (anim != null)
        {
            anim.SetFloat("Turn", turn, dampTime, Time.deltaTime);
            anim.SetFloat("Forward", forward, dampTime, Time.deltaTime);
        }
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //test collision
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            //is obstacle to warrant turn
            if (hit.distance < obstacleRange)
            {
                //if turn is not 0, decide left or right
                if (Mathf.Approximately(turn, 0.0f))
                {
                    //flip a coin and decide, 0 = left 1 = right
                    turn = Random.Range(0, 2) == 0 ? -0.75f : 0.75f;
                }
                //blend causes to move forward and turn, turn quick move forward slow.
                Move(turn, 0.1f);
            } else // no obstacle ok to move forward w/o turning.
            {
                float forwardSpeed = Random.Range(0.05f, 1.0f);
                turn = 0.0f;// set turn to 0.
                //just normal forward movement
                Move(turn, forwardSpeed);
            }
        }
    }
}
