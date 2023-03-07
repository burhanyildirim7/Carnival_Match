using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Popups;
using UnityEngine;

public class JoinTeamContentObjectScript : MonoBehaviour
{
    BaseScene _baseScene;
    // Start is called before the first frame update
    void Start()
    {
        _baseScene = GameObject.FindObjectOfType<BaseScene>();
    }


    public void ShowButton()
    {
        PlayerPrefs.SetString("SearchTeamName", "Clan A");
        PlayerPrefs.SetString("SearchTeamDescription", "Description Tutorial");
        PlayerPrefs.SetString("SearchTeamType", "Open");
        PlayerPrefs.SetString("SearchMinLevelText", "0");
        PlayerPrefs.SetInt("JoinTeamPanel", 1);
        _baseScene.OpenPopup<SettingsPopup>("Popups/SearchTeamDetailPopUp");
    }
}
