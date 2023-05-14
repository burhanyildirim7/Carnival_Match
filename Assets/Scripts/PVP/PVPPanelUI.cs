using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PVPPanelUI : MonoBehaviour
{
    [SerializeField] GameObject _pvpLevelsContentObject,_sagOk,_solOk;
    [SerializeField] List<GameObject> _pvpLevelsContentChildren = new List<GameObject>();

    private int _pvpContentSira;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PVPPanelSagOk()
    {
        //_pvpLevelsContentObject.transform.localPosition = new Vector3(_pvpLevelsContentObject.transform.localPosition.x-670,0,0);
        _pvpLevelsContentObject.transform.DOLocalMove(new Vector3(_pvpLevelsContentObject.transform.localPosition.x - 670, 0, 0),.25f);
        _pvpContentSira++;
        _pvpLevelsContentChildren[_pvpContentSira - 1].GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 1000),.25f);
        _pvpLevelsContentChildren[_pvpContentSira].GetComponent<RectTransform>().DOSizeDelta(new Vector2(900, 1500), .25f);

        _solOk.GetComponent<Button>().interactable = true;
        if (_pvpContentSira == _pvpLevelsContentObject.transform.childCount-1)
        {
            _sagOk.GetComponent<Button>().interactable = false;
        }
        else
        {

        }

    }
    public void PVPPanelSolOk()
    {
        //_pvpLevelsContentObject.transform.localPosition = new Vector3(_pvpLevelsContentObject.transform.localPosition.x + 670, 0, 0);
        _pvpLevelsContentObject.transform.DOLocalMove(new Vector3(_pvpLevelsContentObject.transform.localPosition.x + 670, 0, 0), .25f);
        _pvpContentSira--;
        _pvpLevelsContentChildren[_pvpContentSira + 1].GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 1000), .25f);
        _pvpLevelsContentChildren[_pvpContentSira].GetComponent<RectTransform>().DOSizeDelta(new Vector2(900, 1500), .25f);
        _sagOk.GetComponent<Button>().interactable = true;
        if (_pvpContentSira == 0)
        {
            _solOk.GetComponent<Button>().interactable = false;
        }
        else
        {
                
        }
    }
    public void ContentSiraDegiskeniniGuncelle(int _yenideger)
    {
        _pvpContentSira = _yenideger;

        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsContentChildren.Add(_pvpLevelsContentObject.transform.GetChild(i).gameObject);
        }

        _pvpLevelsContentObject.GetComponent<HorizontalLayoutGroup>().padding.left = 190 + ((Screen.width - 1285) / 2);
        _pvpLevelsContentObject.GetComponent<RectTransform>().sizeDelta = new Vector2(190 + 190 + 900 + ((600 + 70) * (_pvpLevelsContentObject.transform.childCount - 1)), 1500);//x=3291 Iphone12promax
        _pvpLevelsContentObject.transform.localPosition = new Vector3((_pvpLevelsContentObject.GetComponent<RectTransform>().sizeDelta.x / 2) - 640 - ((Screen.width - 1285) / 2), 0, 0);
        _pvpLevelsContentObject.transform.DOLocalMove(new Vector3(_pvpLevelsContentObject.transform.localPosition.x - 670* _pvpContentSira, 0, 0), .01f);

        for (int i = 0; i < _pvpLevelsContentChildren.Count; i++)
        {
            if (i==_pvpContentSira)
            {
                _pvpLevelsContentChildren[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(900, 1500), .01f);
            }
            else
            {
                _pvpLevelsContentChildren[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 1000), .01f);
            }
        }

        if (_pvpContentSira == _pvpLevelsContentObject.transform.childCount - 1)
        {
            _sagOk.GetComponent<Button>().interactable = false;
        }
        else
        {
            _sagOk.GetComponent<Button>().interactable = true;
        }

        if (_pvpContentSira == 0)
        {
            _solOk.GetComponent<Button>().interactable = false;
        }
        else
        {
            _solOk.GetComponent<Button>().interactable = true;
        }
    }
}
