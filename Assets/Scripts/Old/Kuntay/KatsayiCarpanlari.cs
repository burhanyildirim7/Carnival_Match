using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatsayiCarpanlari : MonoBehaviour
{
    [SerializeField] List<GameObject> _soketler = new List<GameObject>();
    [SerializeField] GameObject _aktifObje;
    public float _katsayi;
    public bool _katsayiAktif;

    private float _timer,_sayac;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.instance.isContinue)
        {
            _timer = _timer + Time.deltaTime;
            if (_timer>0.1f)
            {
                _timer = 0;
                _sayac = 0;
                for (int i = 0; i < _soketler.Count; i++)
                {
                    if (_soketler[i].transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk==false)
                    {
                        break;
                    }
                    _sayac++;
                }

                if (_sayac== _soketler.Count)
                {
                    _katsayiAktif = true;
                    _aktifObje.SetActive(true);

                }
                else
                {
                    _katsayiAktif = false;
                    _aktifObje.SetActive(false);

                }
            }


        }


    }
}
