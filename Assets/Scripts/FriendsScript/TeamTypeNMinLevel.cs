using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTypeNMinLevel : MonoBehaviour
{
    [SerializeField] private List<string> _teamTypeList=new List<string>(), _minLevelList = new List<string>();
    [SerializeField] private bool _teamTypeButton, _minLevelButton;
    [SerializeField] private Text _teamTypeText, _minLeveltext;
    private int i=0,j=0;
    public void RightButtonBasma()
    {
        if (_teamTypeButton)
        {
            i++;
            if (i>= _teamTypeList.Count)
            {
                i = _teamTypeList.Count - 1;
            }
            else
            {

            }
            _teamTypeText.text = _teamTypeList[i];
            
        }
        else
        {
            j++;
            if (j >= _minLevelList.Count)
            {
                j = _minLevelList.Count - 1;
            }
            else
            {

            }
            _minLeveltext.text = _minLevelList[j];

        }
    }
    public void LeftButtonBasma()
    {
        if (_teamTypeButton)
        {
            i--;
            if (i <=0)
            {
                i = 0;
            }
            else
            {

            }
            _teamTypeText.text = _teamTypeList[i];

        }
        else
        {
            j--;
            if (j <= 0)
            {
                j = 0;
            }
            else
            {

            }
            _minLeveltext.text = _minLevelList[j];

        }
    }
}
