using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FriendsPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerBoard, _teamBoard, _friendBoard, _leftButton,_middleButton, _rightButton;
    [SerializeField] private Sprite _openedLeftButtonImg, _openedMiddleButtonImg, _openedRightButtonImg, _closedLeftButtonImg, _closedMiddleButtonImg, _closedRightButtonImg;

    public void PlayerPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(true);
        _teamBoard.SetActive(false);
        _friendBoard.SetActive(false);

        _leftButton.GetComponent<Image>().sprite = _openedLeftButtonImg;
        _middleButton.GetComponent<Image>().sprite = _closedMiddleButtonImg;
        _rightButton.GetComponent<Image>().sprite = _closedRightButtonImg;


    }
    public void TeamPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(false);
        _teamBoard.SetActive(true);
        _friendBoard.SetActive(false);

        _leftButton.GetComponent<Image>().sprite = _closedLeftButtonImg;
        _middleButton.GetComponent<Image>().sprite = _openedMiddleButtonImg;
        _rightButton.GetComponent<Image>().sprite = _closedRightButtonImg;


    }
    public void FriendsPanelButton()
    {
        _playerBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _teamBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;
        _friendBoard.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Scrollbar>().value = 1;

        _playerBoard.SetActive(false);
        _teamBoard.SetActive(false);
        _friendBoard.SetActive(true);

        _leftButton.GetComponent<Image>().sprite = _closedLeftButtonImg;
        _middleButton.GetComponent<Image>().sprite = _closedMiddleButtonImg;
        _rightButton.GetComponent<Image>().sprite = _openedRightButtonImg;

    }

}
