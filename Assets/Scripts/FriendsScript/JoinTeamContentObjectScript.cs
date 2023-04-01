using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Popups;
using UnityEngine;
using UnityEngine.UI;

public class JoinTeamContentObjectScript : MonoBehaviour
{
    BaseScene _baseScene;
    [SerializeField] private GameObject _TeamFlag;
    private int _teamFlagNum;
    // Start is called before the first frame update
    void Start()
    {
        _baseScene = GameObject.FindObjectOfType<BaseScene>();
    }


    public void ShowButton()
    {
        _teamFlagNum= int.Parse(_TeamFlag.GetComponent<Image>().sprite.name)-1;
        PlayerPrefs.SetInt("SearchFlagNo",_teamFlagNum);
        Debug.Log("FlagNOO:"+ PlayerPrefs.GetInt("SearchFlagNo"));
        PlayerPrefs.SetString("SearchTeamName", "Team_A_Tutorial");
        PlayerPrefs.SetString("SearchTeamDescription", "Description Tutorial");
        PlayerPrefs.SetString("SearchTeamType", "Open");
        PlayerPrefs.SetString("SearchMinLevelText", "0");
        PlayerPrefs.SetInt("ShowButtonBasildi",1);
        _baseScene.OpenPopup<SettingsPopup>("Popups/SearchTeamDetailPopUp");

    }
}
