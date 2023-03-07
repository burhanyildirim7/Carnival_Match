using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;
using UnityEngine;
using UnityEngine.Assertions;

public class ArrowScript : MonoBehaviour
{
    public static ArrowScript instance;

    private Vector2 sonNokta;

    [SerializeField] private List<GameObject> _trailList = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }


    public void ArrowYerineGec(float degerx, float degery)
    {
        for (int i = 0; i < _trailList.Count; i++)
        {
            _trailList[i].gameObject.SetActive(false);
        }

        gameObject.transform.position = new Vector2(degerx, degery);
        sonNokta = new Vector2(100, degery);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;


        StartCoroutine(AnimasyonBaslat());
        //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator AnimasyonBaslat()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _trailList.Count; i++)
        {
            _trailList[i].gameObject.SetActive(true);
        }

        LeanTween.move(gameObject, sonNokta, 1f).setOnComplete(
                          () =>
                          {
                              gameObject.GetComponent<SpriteRenderer>().enabled = false;

                              for (int i = 0; i < _trailList.Count; i++)
                              {
                                  _trailList[i].gameObject.SetActive(false);
                              }
                          });
    }


}
