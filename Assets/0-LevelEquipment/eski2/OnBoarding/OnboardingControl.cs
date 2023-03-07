using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class OnboardingControl : MonoBehaviour
{
    [SerializeField] GameObject _panel1, _panel2, _hand, _0nokta, _1nokta, _2nokta;

    public bool _devam1=false, _devam2 = false, _devam3 = false;

    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("OnboardingDone")==1)
        {
            _panel1.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

            if (_devam1==false)
            {
                _panel1.SetActive(true);
            }
            else
            {
               
                if (_devam2==false)
                {
                    if (_devam3==false)
                    {
                        _hand.transform.position = _0nokta.transform.position;
                        _devam3 = true;
                        _hand.transform.DOMove(_2nokta.transform.position, 1f).OnComplete(() => _devam3 = false) ;

                    }
                    else
                    {

                    }
                }
                else
                {
                    _panel2.SetActive(true);
                }
            }


        
    }







}
