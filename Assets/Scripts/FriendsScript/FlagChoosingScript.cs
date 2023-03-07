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

        GameObject.Find("TeamFlagButton").GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        _flagChoosePopUp.OnCloseButtonPressed();
        PlayerPrefs.SetInt("FlagNo",_flagNo);
    }

}
