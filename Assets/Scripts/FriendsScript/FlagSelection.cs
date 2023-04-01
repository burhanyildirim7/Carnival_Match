using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagSelection : MonoBehaviour
{
    [SerializeField] public List<Sprite> _flagList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.GetInt("FlagNo")
        if (PlayerPrefs.GetInt("CreationPanel") == 1)
        {
            GetComponent<Image>().sprite = _flagList[PlayerPrefs.GetInt("FlagNo")];

        }
        else if (PlayerPrefs.GetInt("JoinTeamPanel") == 1)
        {
            GetComponent<Image>().sprite = _flagList[PlayerPrefs.GetInt("FlagNo")];

        }
        else if (PlayerPrefs.GetInt("ShowButtonBasildi") == 1)
        {
            GetComponent<Image>().sprite = _flagList[PlayerPrefs.GetInt("SearchFlagNo")];
            PlayerPrefs.SetInt("ShowButtonBasildi",0);
        }
        else
        {
            GetComponent<Image>().sprite = _flagList[0];
        }
    }

}
