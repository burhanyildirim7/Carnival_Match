using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemaDegistir : MonoBehaviour
{
    [SerializeField] GameObject _gunduzObje, _geceObje,_zemin;
    [SerializeField] Material _gunduzMat, _geceMat;

    private void Start()
    {
        if (PlayerPrefs.GetInt("TemaDeger") == 0)
        {
            _gunduzObje.SetActive(true);
            _geceObje.SetActive(false);
            _zemin.transform.GetComponent<Renderer>().material = _geceMat;
        }
        else
        {
            _gunduzObje.SetActive(false);
            _geceObje.SetActive(true);
            _zemin.transform.GetComponent<Renderer>().material = _gunduzMat;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("SesKapat") == 0)
        {
            transform.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            transform.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void TemaDegistirButton()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        if (PlayerPrefs.GetInt("TemaDeger")==0)
        {
            PlayerPrefs.SetInt("TemaDeger",1);
            _gunduzObje.SetActive(true);
            _geceObje.SetActive(false);
            _zemin.transform.GetComponent<Renderer>().material = _geceMat;
        }
        else
        {
            PlayerPrefs.SetInt("TemaDeger", 0);
            _gunduzObje.SetActive(false);
            _geceObje.SetActive(true);
            _zemin.transform.GetComponent<Renderer>().material = _gunduzMat;

        }
    }

}
