using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoruTemasKontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _temas;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="connection")
        {
            _temas = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "connection")
        {
            _temas = true;
            if (transform.parent.GetComponent<BoruScript>()._BoruAktif)
            {
                other.transform.GetComponent<ConnectionControl>()._fiskiyeAktif = true;
            }
            else
            {
                other.transform.GetComponent<ConnectionControl>()._fiskiyeAktif = false;
            }

        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "connection")
        {
            _temas = false;
            other.transform.GetComponent<ConnectionControl>()._fiskiyeAktif = false;
        }
    }
}
