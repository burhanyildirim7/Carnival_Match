using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PreflerIlkKezOlusturuluyor")==0)
        {
            PlayerPrefs.SetInt("PreflerIlkKezOlusturuluyor",1);
            for (int _haritaNo = 0; _haritaNo < 100; _haritaNo++)
            {
                for (int _binaNo = 0; _binaNo < 10; _binaNo++)
                {
                    PlayerPrefs.SetInt("BinaBosAlan" + _haritaNo + _binaNo, 0);

                }
            }
        }
        else
        {
            for (int _haritaNo = 0; _haritaNo < 100; _haritaNo++)
            {
                for (int _binaNo = 0; _binaNo < 10; _binaNo++)
                {
                    Debug.Log("BinaBosAlan" + _haritaNo + _binaNo+"= "+PlayerPrefs.GetInt("BinaBosAlan" + _haritaNo + _binaNo));

                }
            }
        }
    }


}
