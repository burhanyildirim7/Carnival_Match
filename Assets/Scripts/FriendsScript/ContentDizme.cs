using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentDizme : MonoBehaviour
{
    [SerializeField] private GameObject _contentBirimi;
    [SerializeField] public int _contentAdeti;
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
                _geciciContent.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
            }
            else if(_joinTeam)
            {
                _geciciContent = Instantiate(_contentBirimi, transform);
                _geciciContent.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
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
            _geciciContent.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
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
