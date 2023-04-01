using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Popups;


public class TeamPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject _joinBoard, _searchBoard, _createBoard, _leftButton,_middleButton, _rightButton, _showJoinedTeamPanel, _topButton;
    BaseScene _baseScene;

    private void Start()
    {
        _baseScene = GameObject.FindObjectOfType<BaseScene>();
        if (PlayerPrefs.GetInt("CreationPanel") == 1 )
        {
            _joinBoard.SetActive(false);
            _searchBoard.SetActive(false);
            _createBoard.SetActive(true);
            _showJoinedTeamPanel.SetActive(false);
            _topButton.SetActive(true);
            _createBoard.GetComponent<CreatingTeamScript>().StartingProgress();
            Debug.Log("creation");
        }
        else if (PlayerPrefs.GetInt("JoinTeamPanel") == 1)
        {
            _joinBoard.SetActive(false);
            _searchBoard.SetActive(false);
            _createBoard.SetActive(false);
            _showJoinedTeamPanel.SetActive(true);
            _topButton.SetActive(false);
            Debug.Log("join");

        }
        else
        {
            _joinBoard.SetActive(true);
            _searchBoard.SetActive(false);
            _createBoard.SetActive(false);
            _showJoinedTeamPanel.SetActive(false);
            _topButton.SetActive(true);
        }
    }
    public void JoinPanelButton()
    {
        _joinBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _joinBoard.SetActive(true);
        _searchBoard.SetActive(false);
        _createBoard.SetActive(false);

        _leftButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _middleButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _rightButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);


    }
    public void SearchPanelButton()
    {
        _joinBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _joinBoard.SetActive(false);
        _searchBoard.SetActive(true);
        _createBoard.SetActive(false);

        _leftButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _middleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _rightButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);


    }
    public void CreatePanelButton()
    {
        _joinBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _joinBoard.SetActive(false);
        _searchBoard.SetActive(false);
        _createBoard.SetActive(true);

        _leftButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _middleButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _rightButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }

    public void TeamFlagChooseButton()
    {
        _baseScene.OpenPopup<SettingsPopup>("Popups/TeamFlagChoosePopUp");
    }
    public void TeamDetailButton()
    {
        _baseScene.OpenPopup<SettingsPopup>("Popups/TeamDetailPopUp");
    }
    public void JoinTeamDetailButton()
    {
        _baseScene.OpenPopup<SettingsPopup>("Popups/JoinTeamDetailPopUp");
    }

    public void JoiningProgress()
    {
        _joinBoard.SetActive(false);
        _searchBoard.SetActive(false);
        _createBoard.SetActive(false);
        _showJoinedTeamPanel.SetActive(true);
        _topButton.SetActive(false);
    }
    public void JoiningClosingProgress()
    {
        _joinBoard.SetActive(true);
        _searchBoard.SetActive(false);
        _createBoard.SetActive(false);
        _showJoinedTeamPanel.SetActive(false);
        _topButton.SetActive(true);
    }

}
