using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Common;
using UnityEngine;
using UnityEngine.UI;

public class BinaSatinAlma : MonoBehaviour
{
	[SerializeField] private Text _priceText;
    [SerializeField] private GameObject _contentObject,_bosAlanObject;
    [SerializeField] private ParticleSystem coinParticles;
    int coins;
	int _cost;
    public void SatinAlmaButton()
    {
		_cost = int.Parse(_priceText.text);
        coins = PlayerPrefs.GetInt("num_coins");
        if (_cost <= coins)
        {
            PuzzleMatchManager.instance.coinsSystem.SpendCoins(_cost);
            coinParticles.Play();
            SoundManager.instance.PlaySound("CoinsPopButton");
            if (transform.parent.transform.childCount<=1)
            {
                // content'de 1 ve 1den az obje varsa çalışır.
                transform.parent = null;
            }
            else
            {
                PlayerPrefs.SetInt("Acildimi" + _bosAlanObject.GetComponent<BinaKurma>()._haritaNo + _bosAlanObject.GetComponent<BinaKurma>()._binaNo, 1);
                _bosAlanObject.transform.GetChild(1).gameObject.SetActive(true);
                transform.parent.GetChild(1).GetChild(transform.parent.GetChild(1).transform.childCount - 1).gameObject.SetActive(false);
                transform.parent = null;
            }
            _contentObject.GetComponent<ContentAyarlama>().ListeSirasiDuzelt();
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("YETERLİ PARA YOK");
            Debug.Log("cost: "+_cost);
            Debug.Log("coins: "+coins);
        }

    }
  
}
