using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class BinaYerlestirme : MonoBehaviour
{
    [SerializeField] private GameObject _haritaPanelObject;

    public void HaritaPaneliOlustur()
    {
        _haritaPanelObject.SetActive(true);
    }

}
