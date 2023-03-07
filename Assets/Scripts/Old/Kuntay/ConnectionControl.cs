using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionControl : MonoBehaviour
{
    public bool _generatorTemas=false,_field,_fiskiyeAktif;
    private void Start()
    {
        if (_field)
        {
            _generatorTemas = true;
            _fiskiyeAktif = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "generator")
        {
            transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._WORKING = true;
            _generatorTemas = true;
        }
        else if (other.tag == "connection" && _field == false)
        {
            transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Add(other.gameObject.transform.parent.transform.parent.GetChild(0).gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "generator")
        {
            transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._WORKING = true;
            _generatorTemas = true;
        }
        else if (other.tag == "connection" && _field == false)
        {

        }
        else
        {
            if (transform.parent.transform.parent.transform.parent.transform.GetComponent<TurretMergeKontrol>()._objeYerde == false)
            {
                for (int i = 0; i < transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Count; i++)
                {
                    transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler[i].transform.GetComponent<TaretRenkDegistirme>()._WORKING = false;
                }
                transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Clear();
            }
            else
            {
               // transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Remove(other.gameObject.transform.parent.transform.parent.GetChild(0).gameObject);

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "generator")
        {
            transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._WORKING = false;
            _generatorTemas = false;
        }
        else if (other.tag == "connection" && _field==false)
        {
            /* if (transform.parent.transform.parent.transform.parent.transform.GetComponent<TurretMergeKontrol>()._objeYerde==false)
             {
                 for (int i = 0; i < transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Count; i++)
                 {
                     transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler[i].transform.parent.transform.GetComponent<TaretRenkDegistirme>()._WORKING = false;
                 }
                 transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Clear();
             }
             else
             {
                 transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Remove(other.gameObject);

             }*/
            gameObject.transform.parent.transform.parent.GetChild(0).transform.GetComponent<TaretRenkDegistirme>()._WORKING = false;
            for (int i = 0; i < transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Count; i++)
            {
                transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler[i].transform.GetComponent<TaretRenkDegistirme>()._WORKING = false;
            }
            transform.parent.transform.parent.GetChild(0).GetComponent<TaretRenkDegistirme>()._temasEdilenObjeler.Remove(other.gameObject.transform.parent.transform.parent.GetChild(0).gameObject);

        }
    }

}
