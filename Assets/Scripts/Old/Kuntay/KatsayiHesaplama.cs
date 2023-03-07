using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatsayiHesaplama : MonoBehaviour
{
    public float _toplamCarpan;

    private float _timer;
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

            if (_timer>0.1)
            {
                _timer = 0;
                ToplamCarpanHesapla();
            }
        }
    }

    public void ToplamCarpanHesapla()
    {
        float _AraToplam=1;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<KatsayiCarpanlari>()._katsayiAktif)
            {
                _AraToplam = _AraToplam * transform.GetChild(i).gameObject.GetComponent<KatsayiCarpanlari>()._katsayi;
            }
        }
        _toplamCarpan = _AraToplam;
    }


}
