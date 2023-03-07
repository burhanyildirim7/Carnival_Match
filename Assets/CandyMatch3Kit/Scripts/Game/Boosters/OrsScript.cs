using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;
using UnityEngine;

public class OrsScript : MonoBehaviour
{
    public static OrsScript instance;

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
    }


    public void OrsYerineGec(float degerx, float degery)
    {
        for (int i = 0; i < _trailList.Count; i++)
        {
            _trailList[i].gameObject.SetActive(false);
        }

        gameObject.transform.position = new Vector2(degerx, degery);
        sonNokta = new Vector2(degerx, -110);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        StartCoroutine(AnimasyonBaslat());
    }

    private IEnumerator AnimasyonBaslat()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _trailList.Count; i++)
        {
            _trailList[i].gameObject.SetActive(true);
        }

        LeanTween.move(gameObject, sonNokta, 1.1f).setOnComplete(
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
