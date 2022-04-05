using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsPopup : BasePopup
{

    //[SerializeField] UIController uiController;
    [SerializeField] SettingsPopup settingPopup;
    //[SerializeField] private OptionsPopup optionsPopup;


    public void OnSettingsButton()
    {
        Close();
        //optionsPopup.Close();
        settingPopup.Open();

        Debug.Log("settings clicked");
    }

    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        Debug.Log("return to game");
        //uiController.SetGameActive(true);
        //Messenger.Broadcast(GameEvent.POPUP_CLOSED);
        Close();

    }

}
