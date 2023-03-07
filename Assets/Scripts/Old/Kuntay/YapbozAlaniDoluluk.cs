using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YapbozAlaniDoluluk : MonoBehaviour
{
    public bool _soketDoluluk;
    public int _soketNumber,_turretListeDeger=9;

    
    void Start()
    {
        if (PlayerPrefs.GetInt("OyunIlkKezAcildi"  + _soketNumber )== 0)
        {
            PlayerPrefs.SetInt("OyunIlkKezAcildi" + _soketNumber, 1);
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, 9);
        }
        else
        {
            if (PlayerPrefs.GetInt("TurretGetir" + _soketNumber) < 9)
            {
                Instantiate(GameObject.Find("SOKETLER_PARENT").transform.GetComponent<PlayerPrefKontrol>()._turrets[PlayerPrefs.GetInt("TurretGetir" + _soketNumber)], null).transform.position =
        new Vector3(transform.position.x, 0.25f, transform.position.z);
            }
            else
            {

            }

        }

    }
     void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="turret"|| other.tag == "fiskiye")
        {
            switch (other.transform.GetComponent<TurretMergeKontrol>()._turretNum)
            {

                case 1:
                    _turretListeDeger = 0;
                    break;

                case 2:
                    _turretListeDeger = 1;
                    break;

                case 3://fiskiye
                    _turretListeDeger = 7;
                    break;
                case 4:
                    _turretListeDeger = 2;
                    break;

                case 8:
                    _turretListeDeger = 3;
                    break;

                case 16:
                    _turretListeDeger = 4;
                    break;

                case 32:
                    _turretListeDeger = 5;
                    break;

                case 64:
                    _turretListeDeger = 6;
                    break;

                case 65://fiskiye
                    _turretListeDeger = 8;
                    break;



            }
            PlayerPrefs.SetInt("TurretGetir"+ _soketNumber, _turretListeDeger);
        }   
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "turret" || other.tag == "fiskiye")
        {
            switch (other.transform.GetComponent<TurretMergeKontrol>()._turretNum)
            {

                case 1:
                    _turretListeDeger = 0;
                    break;

                case 2:
                    _turretListeDeger = 1;
                    break;

                case 3://fiskiye
                    _turretListeDeger = 7;
                    break;
                case 4:
                    _turretListeDeger = 2;
                    break;

                case 8:
                    _turretListeDeger = 3;
                    break;

                case 16:
                    _turretListeDeger = 4;
                    break;

                case 32:
                    _turretListeDeger = 5;
                    break;

                case 64:
                    _turretListeDeger = 6;
                    break;

                case 65://fiskiye
                    _turretListeDeger = 8;
                    break;
            }
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, _turretListeDeger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "turret" || other.tag == "fiskiye")
        {
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, 9);


        }
    }


}
