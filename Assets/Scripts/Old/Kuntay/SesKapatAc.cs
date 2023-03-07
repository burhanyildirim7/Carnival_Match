using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKapatAc : MonoBehaviour
{
    [SerializeField] GameObject _opened, _closed,_temaSoundObject,_listener;
    [SerializeField] int _prefIsmi;

    void Start()
    {
        if (PlayerPrefs.GetInt("" + _prefIsmi) == 0)
        {
            _opened.SetActive(true);
            _closed.SetActive(false);
            if (_prefIsmi == 1)
            {
                _temaSoundObject.SetActive(true);
            }
            else
            {
               // _listener.transform.GetComponent<AudioListener>().enabled = true;
                PlayerPrefs.SetInt("SesKapat", 0);
                

            }
        }
        else
        {
            _opened.SetActive(false);
            _closed.SetActive(true);
            if (_prefIsmi == 1)
            {
                _temaSoundObject.SetActive(false);
            }
            else
            {
               // _listener.transform.GetComponent<AudioListener>().enabled = false;
                PlayerPrefs.SetInt("SesKapat", 1);
                
            }
        }
    }

    public void FlipState()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        if (PlayerPrefs.GetInt(""+ _prefIsmi)==0)
        {
            PlayerPrefs.SetInt("" + _prefIsmi, 1);
            _opened.SetActive(false);
            _closed.SetActive(true);
            if (_prefIsmi==1)
            {
                _temaSoundObject.SetActive(false);
            }
            else
            {
               // _listener.transform.GetComponent<AudioListener>().enabled =false;
                PlayerPrefs.SetInt("SesKapat",1);
                
            }
        }
        else
        {
            PlayerPrefs.SetInt("" + _prefIsmi, 0);
            _opened.SetActive(true);
            _closed.SetActive(false);
            if (_prefIsmi == 1)
            {
                _temaSoundObject.SetActive(true);
            }
            else
            {
                //_listener.transform.GetComponent<AudioListener>().enabled = true;
                PlayerPrefs.SetInt("SesKapat", 0);
                
            }
        }
    }

}
