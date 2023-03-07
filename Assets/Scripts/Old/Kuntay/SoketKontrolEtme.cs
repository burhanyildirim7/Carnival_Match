using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoketKontrolEtme : MonoBehaviour
{
    [SerializeField] public GameObject _serbestAlanObjesi, _yasakliAlanObjesi;

    public bool _objeYerlestirilebilir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="soket" && transform.parent.parent.tag=="turret")
        {
            if (other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk==false)
            {
                _objeYerlestirilebilir = true;
            }
            else
            {
                _objeYerlestirilebilir = false;
            }
        }
        else if (other.tag == "boruSoket" && transform.parent.parent.tag == "fiskiye")
        {
            if (other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk == false)
            {
                _objeYerlestirilebilir = true;
            }
            else
            {
                _objeYerlestirilebilir = false;
            }
        }
        else if (other.tag == "merge")
        {
            if (other.transform.GetComponent<mergeAlaniDoluluk>()._doluluk == false)
            {
                _objeYerlestirilebilir = true;
            }
            else
            {
                _objeYerlestirilebilir = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "soket")
        {
            if (_objeYerlestirilebilir==true)
            {
                _objeYerlestirilebilir = false;
            }
        }
    }

}
