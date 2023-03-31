using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FriendsPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerBoard, _teamBoard, _friendBoard, _leftButton,_middleButton, _rightButton;

    public void PlayerPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(true);
        _teamBoard.SetActive(false);
        _friendBoard.SetActive(false);
        
        _leftButton.GetComponent<Image>().color= new Color(1,1,1,1);
        _middleButton.GetComponent<Image>().color = new Color(1,1,1, .5f);
        _rightButton.GetComponent<Image>().color = new Color(1,1,1, .5f);


    }
    public void TeamPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(false);
        _teamBoard.SetActive(true);
        _friendBoard.SetActive(false);

        _leftButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _middleButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _rightButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);


    }
    public void FriendsPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(false);
        _teamBoard.SetActive(false);
        _friendBoard.SetActive(true);

        _leftButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _middleButton.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        _rightButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);


    }

}
