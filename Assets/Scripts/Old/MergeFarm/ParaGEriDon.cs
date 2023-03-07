using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaGEriDon : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="paraHedef")
        {


            transform.localPosition = Vector3.zero;
            transform.parent.GetChild(1).gameObject.SetActive(false) ;
            transform.gameObject.SetActive(false);


        }
    }


}
