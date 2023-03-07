using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretMergeKontrol : MonoBehaviour
{
    [SerializeField] GameObject _nextTurret;
    [SerializeField] public int _turretNum;
    public bool _mergeEdilebilir, _objeYerde,_field,_fiskiye;
    private GameObject _mergeTahtası,_geciciTurret;
    public List<GameObject> _hasatObjesi=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_objeYerde)
        {
        }
        else
        {
            if (_turretNum >= 64)
            {

            }
            else
            {
                /*
                if (_field)
                {
                    if (other.tag == "turret")
                    {
                        if (other.transform.GetComponent<TurretMergeKontrol>()._mergeEdilebilir)
                        {
                            if (_turretNum == other.gameObject.transform.GetComponent<TurretMergeKontrol>()._turretNum)
                            {
                                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                                _geciciTurret = Instantiate(_nextTurret, null);
                                if (_geciciTurret.transform.GetComponent<TurretMergeKontrol>()._turretNum == 2)
                                {
                                    _geciciTurret.transform.position = new Vector3(other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.x + 0.5f,
                                        other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.y,
                                        other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.z);
                                }
                                else
                                {
                                    _geciciTurret.transform.position = other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position;

                                }
                                _geciciTurret.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                _geciciTurret.transform.DOScale(1, 0.2f);
                                GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._yakalananTurret = GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._geciciKonum;
                                Destroy(other.gameObject);
                                Destroy(gameObject);
                            }
                            else
                            {

                            }

                        }
                    }

                }
                else if (_fiskiye)
                {
                    if (other.tag == "fiskiye")
                    {
                        if (other.transform.GetComponent<TurretMergeKontrol>()._mergeEdilebilir)
                        {
                            if (_turretNum == other.gameObject.transform.GetComponent<TurretMergeKontrol>()._turretNum)
                            {
                                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                                _geciciTurret = Instantiate(_nextTurret, null);
                                if (_geciciTurret.transform.GetComponent<TurretMergeKontrol>()._turretNum == 2)
                                {
                                    _geciciTurret.transform.position = new Vector3(other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.x + 0.5f,
                                        other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.y,
                                        other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.z);
                                }
                                else
                                {
                                    _geciciTurret.transform.position = other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position;

                                }
                                _geciciTurret.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                _geciciTurret.transform.DOScale(1, 0.2f);
                                GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._yakalananTurret = GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._geciciKonum;
                                Destroy(other.gameObject);
                                Destroy(gameObject);
                            }
                            else
                            {

                            }

                        }
                    }

                }
                else
                {

                }
                */
                if (other.tag == transform.tag)
                {
                    if (other.transform.GetComponent<TurretMergeKontrol>()._mergeEdilebilir)
                    {
                        if (_turretNum == other.gameObject.transform.GetComponent<TurretMergeKontrol>()._turretNum)
                        {
                            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                            _geciciTurret = Instantiate(_nextTurret, null);
                            if (_geciciTurret.transform.GetComponent<TurretMergeKontrol>()._turretNum == 2)
                            {
                                _geciciTurret.transform.position = new Vector3(other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.x + 0.5f,
                                    other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.y,
                                    other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position.z);
                            }
                            else
                            {
                                _geciciTurret.transform.position = other.gameObject.transform.GetComponent<TurretMergeKontrol>().transform.position;

                            }
                            _geciciTurret.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            _geciciTurret.transform.DOScale(1, 0.2f);
                            GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._yakalananTurret = GameObject.Find("RAY_CONTROLLER").transform.GetComponent<RayKodlari>()._geciciKonum;
                            Destroy(other.gameObject);
                            Destroy(gameObject);
                        }
                        else
                        {

                        }

                    }
                }


            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "merge")
        {
            _mergeTahtası = other.gameObject;

        }
        else if (other.tag == "soket")
        {
            _mergeEdilebilir = false;
        }
        else if (other.tag=="turret")
        {
            if (_turretNum == 64) //   MAX TURRET MERGE LEVELİ
            {
                _mergeEdilebilir = false;
            }
        }

        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "merge")
        {
            _objeYerde = false;
            if (other.transform.GetComponent<mergeAlaniDoluluk>()._doluluk == true)
            {
                other.transform.GetComponent<mergeAlaniDoluluk>()._doluluk = false;
            }
            else
            {

            }
        }
        if (other.tag=="soket")
        {
            _objeYerde = false;
        }
    }

}
