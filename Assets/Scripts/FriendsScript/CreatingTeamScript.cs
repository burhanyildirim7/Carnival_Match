using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreatingTeamScript : MonoBehaviour
{
    [SerializeField] GameObject _creationPanel, _showTeamPanel,_backGround,_topButton;
    [SerializeField] Text _teamName, _teamDescription,_teamType,_minLevel,_clanNameText;

    private void Start()
    {
        StartingProgress();
    }
    public void StartingProgress()
    {
        if (PlayerPrefs.GetInt("CreationPanel")==1)
        {
            
            _showTeamPanel.SetActive(true);
            _backGround.GetComponent<Image>().enabled = false;
            _topButton.SetActive(false);
            _clanNameText.text = PlayerPrefs.GetString("TeamName");
            _creationPanel.SetActive(false);
        }
        else
        {
            _showTeamPanel.SetActive(false);
            _backGround.GetComponent<Image>().enabled = true;
            _topButton.SetActive(true);
            _creationPanel.SetActive(true);
        }
    }

    public void CreatButton()
    {

        if (_teamName.text!=""&&_teamDescription.text!="")
        {
            PlayerPrefs.SetInt("CreationPanel", 1);
            PlayerPrefs.SetString("TeamName", _teamName.text);
            PlayerPrefs.SetString("TeamDescription", _teamDescription.text);
            PlayerPrefs.SetString("TeamType", _teamType.text);
            PlayerPrefs.SetString("MinLevelText", _minLevel.text);
            _creationPanel.SetActive(false);
            _showTeamPanel.SetActive(true);
            _backGround.GetComponent<Image>().enabled = false;
            _topButton.SetActive(false);
            _clanNameText.text = PlayerPrefs.GetString("TeamName");

        }

    }
    public void TeamClosingProgress()
    {
        _creationPanel.SetActive(true);
        _showTeamPanel.SetActive(false);
        _backGround.GetComponent<Image>().enabled = true;
        _topButton.SetActive(true);
        _teamName.text = "";
        _teamDescription.text = "";
    }

}
