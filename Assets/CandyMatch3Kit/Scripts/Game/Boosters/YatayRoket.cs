using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatayRoket : MonoBehaviour
{
    [SerializeField] private GameObject _sag;
    [SerializeField] private GameObject _sol;

    private Vector2 sonNoktaYukari;
    private Vector2 sonNoktaAsagi;

    void Start()
    {
        sonNoktaYukari = new Vector2(transform.position.x, 110f);

        StartCoroutine(AnimasyonBaslat());
    }

    private void FixedUpdate()
    {
        _sag.transform.Translate(Vector2.right * 200f * Time.deltaTime);
        _sol.transform.Translate(Vector2.left * 200f * Time.deltaTime);
    }


    private void YukariCalıstir()
    {

        LeanTween.move(_sag, sonNoktaYukari, 1.1f).setOnComplete(
                         () =>
                         {
                             gameObject.GetComponent<SpriteRenderer>().enabled = false;

                         });
    }

    private IEnumerator AnimasyonBaslat()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
