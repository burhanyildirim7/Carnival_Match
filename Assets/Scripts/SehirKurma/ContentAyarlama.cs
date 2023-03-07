using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAyarlama : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ListeSirasiDuzelt()
    {

        for (int sira = 0; sira < transform.childCount; sira++)
        {
            transform.GetChild(sira).transform.localPosition = new Vector3(transform.GetChild(sira).transform.localPosition.x, -125-sira*250,0);
        }

    }
}
