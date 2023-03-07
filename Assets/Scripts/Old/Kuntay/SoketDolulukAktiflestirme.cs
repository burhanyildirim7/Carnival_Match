using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoketDolulukAktiflestirme : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "soket"|| other.tag == "boruSoket")
        {
            if (other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk == false)
            {
                other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk = true;
            }
            else
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "soket" || other.tag == "boruSoket")
        {
            if (other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk == true)
            {
                other.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk = false;
            }
            else
            {

            }
        }
    }
}
