using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FiskiyeKontrol : MonoBehaviour
{
    [SerializeField] GameObject _fiskiyeFX,_workingKontrolObjesi;
    [SerializeField] Animator _fiskiyeAnim;
    [SerializeField] List<GameObject> _temasObjeleri=new List<GameObject>();

    public bool _work=false;

    private float _sayac=0,_sayac2=0;
    void Update()
    {
        _sayac += Time.deltaTime;
        if (_sayac>0.2f)
        {
            _sayac = 0;
            _sayac2 = 0;

            for (int i = 0; i < _temasObjeleri.Count; i++)
            {
                if (_temasObjeleri[i].GetComponent<ConnectionControl>()._fiskiyeAktif)
                {
                    _work = true;
                    break;
                }
                _sayac2++;
            }
            if (_sayac2==_temasObjeleri.Count)
            {
                _work = false;
            }

        }

        if (GameObject.Find("SOKETLER_PARENT").transform.GetComponent<AnaSoketKontrol>()._SYSTEMCONTROL && _work)
        {

            _fiskiyeFX.SetActive(true);
            _fiskiyeAnim.SetBool("run",true);
        }
        else if (_workingKontrolObjesi.GetComponent<TaretRenkDegistirme>()._WORKING)
        {
            _fiskiyeFX.SetActive(true);
            _fiskiyeAnim.SetBool("run", true);
        }
        else
        {
            _fiskiyeFX.SetActive(false);
            _fiskiyeAnim.SetBool("run", false);
        }
    }
}
