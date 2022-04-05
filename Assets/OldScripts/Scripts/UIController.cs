using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIController : MonoBehaviour
{
    //private int score = 0;
    [SerializeField] private TextMeshProUGUI ScoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private PlayerCharacter pc;
    public bool isOpen = false;
    private float healthFloat = 1.0f;
    private int popupsOpen = 0;
    [SerializeField] private GameOverPopup gameOverPopup;

    public void ShowGameOverPopup()
    {
        gameOverPopup.Open();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(healthFloat);
        //healthBar.fillAmount = 1;
        // healthBar.color = Color.green;
        SetGameActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
       // UpdateScore(score);
        if (Input.GetKeyDown(KeyCode.Escape) && popupsOpen == 0)
        {
            

            optionsPopup.Open();
            
        }

    }

    private void UpdateHealth(float healthFloat)
    {

        healthBar.fillAmount = healthFloat;
        healthBar.color = Color.Lerp(Color.red, Color.green, healthFloat);
    }
    private void OnHealthChanged()
    {
        healthFloat = pc.getHealth();
        UpdateHealth(healthFloat);

        
    }
    private void OnPopupClosed()
    {
        popupsOpen--;
        if (popupsOpen == 0)
        {
            SetGameActive(true);
        }
    }
    private void OnPopupOpened()
    {
        if (popupsOpen == 0)
        {
            SetGameActive(false);
        }
        popupsOpen++;
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);

    }
    public void UpdateScore(int newScore)
    {
        ScoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
            Time.timeScale = 1;                        // unpause the game 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // show the cursor 
            crossHair.gameObject.SetActive(true);      // show the crosshair 
            isOpen = false;
        }
        else
        {
            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            Time.timeScale = 0;                       // pause the game 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;   // show cursor // show the cursor 
            crossHair.gameObject.SetActive(false);    // turn off the crosshair 
            isOpen = true;
        }
    }

}
