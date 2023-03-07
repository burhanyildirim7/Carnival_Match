using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class SapScript : MonoBehaviour
{
    [SerializeField] List<GameObject> _sebzeler = new List<GameObject>();
    [SerializeField] GameObject _hasatEdilebilirSebzeFX,_paraObjesi,_kamyonKonumu;
    private int _hasatDegeri;
    private float _sapScale, _sebzeScale;
    public bool _hasatEdilebilir;
    void Start()
    {
        transform.localScale = new Vector3(.2f,.2f,.2f);
        for (int i = 0; i < _sebzeler.Count; i++)
        {
            _sebzeler[i].transform.localScale = new Vector3(.2f, .2f, .2f);
        }
        _hasatDegeri = 0;
    }

    void Update()
    {
        if (transform.parent.parent.parent.GetComponent<TurretMergeKontrol>()._objeYerde==false)
        {
            if (_hasatEdilebilir)
            {
                transform.localScale = new Vector3(.2f, .2f, .2f);
                for (int i = 0; i < _sebzeler.Count; i++)
                {
                    _sebzeler[i].transform.localScale = new Vector3(.2f, .2f, .2f);
                }
                _hasatDegeri = 0;

                _kamyonKonumu = GameObject.Find("KAMYON_NOKTASI").transform.gameObject;
                _hasatEdilebilir = false;
                 transform.tag = "hamSebze";
                _paraObjesi.SetActive(true);
                _paraObjesi.transform.DOJump(_kamyonKonumu.transform.position, 5, 1, 1f).OnComplete(() => ParaGeriDon());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="sulamaObjesi" && _hasatEdilebilir==false)
        {
            _hasatDegeri += 10;
            _sapScale = ((0.008f) * _hasatDegeri) + 0.2f;
            transform.localScale = new Vector3(_sapScale, _sapScale, _sapScale);
            if (_hasatDegeri>49)
            {
                _sebzeScale = ((0.0144f) * _hasatDegeri) - 0.72f;
                for (int i = 0; i < _sebzeler.Count; i++)
                {
                    _sebzeler[i].transform.localScale = new Vector3(_sebzeScale, _sebzeScale, _sebzeScale);
                }
            }
            if (_hasatDegeri>=120)
            {
                _hasatEdilebilir = true;
                transform.tag = "collectible";
                _hasatEdilebilirSebzeFX.SetActive(true);

            }
            else
            {
                _hasatEdilebilirSebzeFX.SetActive(false);
            }
        }
        if (other.tag=="toplayici" && _hasatEdilebilir && transform.tag == "collectible")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            transform.localScale = new Vector3(.2f, .2f, .2f);
            for (int i = 0; i < _sebzeler.Count; i++)
            {
                _sebzeler[i].transform.localScale = new Vector3(.2f, .2f, .2f);
            }
            _hasatDegeri = 0;

            _kamyonKonumu = GameObject.Find("KAMYON_NOKTASI").transform.gameObject;
            _hasatEdilebilir = false;
            transform.tag = "hamSebze";
            _paraObjesi.SetActive(true);
            _paraObjesi.transform.DOJump(_kamyonKonumu.transform.position,5,1,1f).OnComplete(()=>ParaGeriDon());
        }
    }
    private void ParaGeriDon()
    {
        _paraObjesi.transform.localPosition=Vector3.zero;
        _paraObjesi.SetActive(false);
    }

}
