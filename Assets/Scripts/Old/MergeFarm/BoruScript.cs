using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoruScript : MonoBehaviour
{
    [SerializeField] GameObject _suDamlasi,_temasObjesi1,_temasObjesi2;
    [SerializeField] Material _deaktifRenk, _aktifRenk;


    private float _damlaBoyutu,_sayac;
    private GameObject _tempDamla;

    public bool _BoruAktif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SOKETLER_PARENT").transform.GetComponent<AnaSoketKontrol>()._SYSTEMCONTROL &&
            _temasObjesi1.GetComponent<BoruTemasKontrol>()._temas &&
            _temasObjesi2.GetComponent<BoruTemasKontrol>()._temas)
        {
            _BoruAktif = true;
            _sayac += Time.deltaTime;
            if (_sayac>0.3f)
            {
                _sayac = 0;
                transform.GetComponent<Renderer>().material = _aktifRenk;
                //_damlaBoyutu = Random.Range(0.5f, .8f);
                _tempDamla = Instantiate(_suDamlasi, transform);
                //_tempDamla.transform.localScale = new Vector3(_damlaBoyutu, _damlaBoyutu, _damlaBoyutu);
                _tempDamla.transform.localPosition = new Vector3(0, -1.2f, 0);
                _tempDamla.transform.DOLocalMove(new Vector3(0, 1.2f, 0), 0.8f);

            }
        }
        else
        {
            _BoruAktif = false;
            transform.GetComponent<Renderer>().material = _deaktifRenk;
        }
    }
}
