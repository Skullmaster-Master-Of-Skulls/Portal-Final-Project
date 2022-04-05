using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private bool paused = false;
    [SerializeField]
    TextMeshProUGUI count;
    private Coroutine myCoroutine;
    int secondsRemaining = 15;
    // Start is called before the first frame update
    void Start()
    {
        myCoroutine = StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (paused == false && secondsRemaining != 0)
            {
                StopCoroutine(myCoroutine);
                paused = true;
            }
            else if (paused == true && secondsRemaining != 0)
            {
                myCoroutine = StartCoroutine(Countdown());
                paused = false;
            }

        }
    }

    IEnumerator Countdown()
    {
        for (; secondsRemaining >= 0; secondsRemaining--)
        {
            count.SetText("" + secondsRemaining);
            yield return new WaitForSeconds(1);
        }
        count.SetText("Game Over!");

    }

}

