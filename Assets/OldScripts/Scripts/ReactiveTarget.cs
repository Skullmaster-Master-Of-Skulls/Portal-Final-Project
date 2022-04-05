using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeadEvent () {
        Destroy (this.gameObject);
    }

    public void ReactToHit()
    {
        WanderingAI enemyAI = GetComponent<WanderingAI>();
        Animator enemyAnimator = GetComponent<Animator> ();
        if (enemyAnimator != null) {
            enemyAnimator.SetTrigger("Die");
        }
        if (enemyAI != null)
        {
            enemyAI.ChangeState(EnemyStates.dead);
            if (isAlive)
            {
                Messenger.Broadcast(GameEvent.ENEMY_DEAD);
                isAlive = false;
            }
        }
        
      //  StartCoroutine(Die());

    }

    private IEnumerator Die()
    {
       // iTween.RotateAdd(this.gameObject, new Vector3(-75, 0, 0), 1);
        yield return new WaitForSeconds(5);
        //Destroy(this.gameObject);
        // Enemy falls over and disappears after two seconds 
    }
}
