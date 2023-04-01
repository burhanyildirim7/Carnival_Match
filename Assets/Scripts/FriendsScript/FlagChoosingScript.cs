using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Popups;
using UnityEngine;
using UnityEngine.UI;

public class FlagChoosingScript : MonoBehaviour
{
    [SerializeField] TeamFlagChoosing _flagChoosePopUp;
    [SerializeField] private int _flagNo;
    public void ChooseFlagButton()
    {

        _flagChoosePopUp.OnCloseButtonPressed();
        PlayerPrefs.SetInt("TempFlagNo",_flagNo);
        Debug.Log("TempBayrakNo:" + PlayerPrefs.GetInt("TempFlagNo"));

        GameObject.Find("TeamFlagButton").GetComponent<Image>().sprite = GetComponent<Image>().sprite;

    }

}
