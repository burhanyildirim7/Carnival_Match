using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaKurma : MonoBehaviour
{

    [SerializeField] GameObject _bosAlan, _tamBina,_buildingChoicePanel,_productPanel;
    [SerializeField] public int _binaNo,_haritaNo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //BOŞ ALAN FONKSİYONU
    public void BosAlanButtonFonk()
    {

        _buildingChoicePanel.SetActive(true);
        //PlayerPrefs.SetInt("BinaBosAlan"+_haritaNo+_binaNo,1);
        GameObject.Find("ToDoListButton").GetComponent<ToDoListAcma>().AnaEkrandaPanelAcma();
    }


    public void TamBinaButtonFonk()
    {

        _productPanel.SetActive(true);
        GameObject.Find("ToDoListButton").GetComponent<ToDoListAcma>().AnaEkrandaPanelAcma();

    }

}
