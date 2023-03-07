using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentDizme : MonoBehaviour
{
    [SerializeField] private GameObject _contentBirimi;
    [SerializeField] public int _contentAdeti;
    [SerializeField] private Sprite _rank1Img, _rank1Num, _rank2Img, _rank2Num, _rank3Img, _rank3Num;
    [SerializeField] private bool _players, _teams, _friends, _searchTeam,_joinTeam;
    [SerializeField] private InputField _inputTextArea;
    private GameObject _geciciContent;
    private int _tempChildCount;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,220* _contentAdeti+20);
        for (int i = 0; i < _contentAdeti; i++)
        {
            
            if (_searchTeam==false && _joinTeam==false)
            {
                _geciciContent = Instantiate(_contentBirimi, transform);

                if (i < 3)
                {
                    _geciciContent.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    _geciciContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    switch (i)
                    {

                        case 0:
                            _geciciContent.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _rank1Img;
                            _geciciContent.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().sprite = _rank1Num;
                            break;
                        case 1:
                            _geciciContent.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _rank2Img;
                            _geciciContent.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().sprite = _rank2Num;
                            break;
                        case 2:
                            _geciciContent.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _rank3Img;
                            _geciciContent.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().sprite = _rank3Num;
                            break;
                    }

                }
                else
                {
                    _geciciContent.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
                    _geciciContent.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    _geciciContent.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    _geciciContent.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
                }

            }
            else if(_joinTeam)
            {

                    _geciciContent = Instantiate(_contentBirimi, transform);

            }
            else
            {

            }
        }
    }


    public void SearchTeamButton()
    {
        if (transform.childCount>0)
        {
            _tempChildCount = transform.childCount;
            for (int i = _tempChildCount-1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < _contentAdeti; i++)
        {
            _geciciContent = Instantiate(_contentBirimi, transform);
        }

    }
    public void InputTextTemizleme()
    {
        if (transform.childCount > 0)
        {
            _tempChildCount = transform.childCount;
            for (int i = _tempChildCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        _inputTextArea.text = "";
    }

}
