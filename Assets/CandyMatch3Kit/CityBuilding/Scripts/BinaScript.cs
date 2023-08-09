using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BinaScript : MonoBehaviour
{
    [SerializeField] private int _haritaNumarasi;
    [SerializeField] private int _binaNumarasi;

    [SerializeField] private BinaYerlestirme _binaYerlestirmeScript;

    public void BosAlanButtonClick()
    {
        _binaYerlestirmeScript.HaritaPaneliOlustur();
    }
}
