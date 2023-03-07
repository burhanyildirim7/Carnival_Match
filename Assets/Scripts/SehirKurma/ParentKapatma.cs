using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentKapatma : MonoBehaviour
{
    public void ParentKapatmaButton()
    {

        transform.parent.gameObject.SetActive(false);

    }
}
