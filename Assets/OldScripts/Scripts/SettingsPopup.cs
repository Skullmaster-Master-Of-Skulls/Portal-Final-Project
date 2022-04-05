using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPopup : BasePopup
{

    [SerializeField] private TextMeshProUGUI difficultyLabel;
    [SerializeField] public Slider difficultySlider;
    //[SerializeField] SettingsPopup settingPopup;
    [SerializeField] private OptionsPopup optionsPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnOKButton()
    {
        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
        Messenger<int>.Broadcast(GameEvent.DIFFICULTY_CHANGED, (int)difficultySlider.value);
        
        optionsPopup.Open();

        //settingPopup.Close();
        Close();
    }

    public void OncancelButton()
    {
        optionsPopup.Open();
        //settingPopup.Close();
        Close();
        //Messenger.Broadcast(GameEvent.POPUP_CLOSED);
    }

    override
    public void Open()
    {

        difficultySlider.value = PlayerPrefs.GetInt("difficulty", 1);
        UpdateDifficulty(difficultySlider.value);
        gameObject.SetActive(true);
        Messenger.Broadcast(GameEvent.POPUP_OPENED);
    }
    public void UpdateDifficulty(float difficulty)
    {
        difficultyLabel.text = "Difficulty: " +((int)difficulty).ToString();
    }

    public void OnDifficultyValueChanged(float difficulty)
    {
        UpdateDifficulty(difficulty);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
