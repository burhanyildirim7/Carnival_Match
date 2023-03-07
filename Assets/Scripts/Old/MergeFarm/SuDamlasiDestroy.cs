using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuDamlasiDestroy : MonoBehaviour
{
    void Start()
    {

        Destroy(gameObject, 0.82f);
        transform.DOScale(new Vector3(.7f, .7f, .7f), .01f).OnComplete(()=> transform.DOScale(new Vector3(.85f,.85f,.85f), .4f).OnComplete(()=> transform.DOScale(new Vector3(.2f, .2f, .2f), .4f)));
        
    }
}
