using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class HaritaPanelUI : MonoBehaviour
{
    [SerializeField] GameObject _pvpLevelsContentObject, _sagOk, _solOk;
    [SerializeField] List<GameObject> _pvpLevelsContentChildren = new List<GameObject>();

    [SerializeField] private GameObject _bina_1_1_image;

    private int _pvpContentSira;

    private float _xDeger;

    private void Start()
    {
        _xDeger = _pvpLevelsContentObject.transform.localPosition.x;

        _pvpContentSira = 0;

        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsContentChildren.Add(_pvpLevelsContentObject.transform.GetChild(i).gameObject);
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

        AcilisBinaGorselYerlestir();
    }
    public void HaritaPanelSagOk()
    {
        _xDeger = _xDeger - 470;
        _pvpLevelsContentObject.transform.DOLocalMove(new Vector3(_xDeger, 0, 0), .25f);
        _pvpContentSira++;
        _pvpLevelsContentChildren[_pvpContentSira - 1].GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 400), .25f);
        _pvpLevelsContentChildren[_pvpContentSira].GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 600), .25f);

        _solOk.GetComponent<Button>().interactable = true;
        if (_pvpContentSira == _pvpLevelsContentObject.transform.childCount - 1)
        {
            _sagOk.GetComponent<Button>().interactable = false;
        }
        else
        {

        }

    }
    public void HaritaPanelSolOk()
    {
        _xDeger = _xDeger + 470;
        _pvpLevelsContentObject.transform.DOLocalMove(new Vector3(_xDeger, 0, 0), .25f);
        _pvpContentSira--;
        _pvpLevelsContentChildren[_pvpContentSira + 1].GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 400), .25f);
        _pvpLevelsContentChildren[_pvpContentSira].GetComponent<RectTransform>().DOSizeDelta(new Vector2(600, 600), .25f);
        _sagOk.GetComponent<Button>().interactable = true;
        if (_pvpContentSira == 0)
        {
            _solOk.GetComponent<Button>().interactable = false;
        }
        else
        {

        }
    }

    private void AcilisBinaGorselYerlestir()
    {
        GameObject imageBase = Resources.Load("Binalar/Bina_1_1/Bina_1_1") as GameObject;
        GameObject image = Instantiate(imageBase) as GameObject;

        image.transform.SetParent(_bina_1_1_image.transform, false);
    }
}

