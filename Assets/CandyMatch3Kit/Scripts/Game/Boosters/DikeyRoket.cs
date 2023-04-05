using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DikeyRoket : MonoBehaviour
{
    [SerializeField] private GameObject _yukari;
    [SerializeField] private GameObject _asagi;

    private Vector2 sonNoktaYukari;
    private Vector2 sonNoktaAsagi;

    void Start()
    {
        sonNoktaYukari = new Vector2(transform.position.x, 110f);

        StartCoroutine(AnimasyonBaslat());
    }

    private void FixedUpdate()
    {
        _yukari.transform.Translate(Vector2.up * 200f * Time.deltaTime);
        _asagi.transform.Translate(Vector2.down * 200f * Time.deltaTime);
    }


    private void YukariCalıstir()
    {

        LeanTween.move(_yukari, sonNoktaYukari, 1.1f).setOnComplete(
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
