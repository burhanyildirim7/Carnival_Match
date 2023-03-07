using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using DG.Tweening;
//using Facebook.Unity.Example;
using UnityEngine.UIElements;

public class RayKodlari : MonoBehaviour
{
    [SerializeField] public GameObject _paraAlani, _geciciKonum, _paraToplayici;
    [SerializeField] LayerMask _gorundlayerMask, _soketLayerMask, _mergeLayerMask, _paraAlaniLayer, _turretYakalaLayer;
    public GameObject _yakalananTurret;
    private Transform _turretinYakalandigiKonum;
    private int _sayac, _sayac2,_tarlaSayac;
    private GameObject _geciciTarla;
    private bool _tarlaHazir;
    // Start is called before the first frame update
    void Start()
    {
        _yakalananTurret = _geciciKonum;
        _turretinYakalandigiKonum = _geciciKonum.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isContinue)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _turretYakalaLayer))
                {
                    if (hitInfo.transform.gameObject.tag == "turret")
                    {
                        _geciciTarla = hitInfo.transform.gameObject;
                        for (int i = 0; i < _geciciTarla.transform.GetComponent<TurretMergeKontrol>()._hasatObjesi.Count; i++)
                        {
                            if (_geciciTarla.transform.GetComponent<TurretMergeKontrol>()._hasatObjesi[i].GetComponent<SapScript>()._hasatEdilebilir)
                            {
                                _tarlaSayac++;
                                break;
                            }

                        }
                        if (_tarlaSayac>0)
                        {
                            _tarlaSayac = 0;
                            _tarlaHazir = false;
                        }
                        else
                        {
                            Debug.Log("TARLA HAZIIRRRR");
                            _tarlaSayac = 0;
                            _tarlaHazir = true;
                        }
                        if (_tarlaHazir)
                        {
                            _turretinYakalandigiKonum.transform.position = new Vector3(hitInfo.transform.position.x, .25f, hitInfo.transform.position.z);
                            _yakalananTurret = hitInfo.transform.gameObject;
                        }
                        else
                        {
                            Debug.Log("TUTAMIIIYYOOOOM");

                        }
                    }
                    else if (hitInfo.transform.gameObject.tag == "fiskiye")
                    {
                        _turretinYakalandigiKonum.transform.position = new Vector3(hitInfo.transform.position.x, .25f, hitInfo.transform.position.z);
                        _yakalananTurret = hitInfo.transform.gameObject;
                    }
                    else
                    {

                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _gorundlayerMask))
                {
                    _yakalananTurret.transform.position = new Vector3(hitInfo.point.x, 2, hitInfo.point.z);
                }
                if (Physics.Raycast(ray, out RaycastHit hitParaAlani, float.MaxValue, _paraAlaniLayer))
                {
                    if (hitParaAlani.transform.gameObject == _paraAlani)
                    {
                        _paraToplayici.transform.position = new Vector3(hitParaAlani.point.x, 0, hitParaAlani.point.z);
                    }
                    else
                    {

                    }
                }
                if (Physics.Raycast(ray, out RaycastHit hitSoketInfo, float.MaxValue, _soketLayerMask))
                {
                    _sayac2 = 0;
                    for (int i = 0; i < _yakalananTurret.transform.GetChild(0).transform.childCount; i++)
                    {
                        if (_yakalananTurret.transform.GetChild(0).GetChild(i).GetComponent<SoketKontrolEtme>()._objeYerlestirilebilir)
                        {
                            _sayac2++;
                        }

                    }
                    if (_sayac2 == _yakalananTurret.transform.GetChild(0).transform.childCount)
                    {
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._serbestAlanObjesi.SetActive(true);
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._yasakliAlanObjesi.SetActive(false);
                    }
                    else
                    {
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._serbestAlanObjesi.SetActive(false);
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._yasakliAlanObjesi.SetActive(true);
                    }
                }
                else if (Physics.Raycast(ray, out RaycastHit hitMergeSoketInfo, float.MaxValue, _mergeLayerMask))
                {

                    if (_yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._objeYerlestirilebilir)
                    {
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._serbestAlanObjesi.SetActive(true);
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._yasakliAlanObjesi.SetActive(false);
                    }
                    else
                    {
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._serbestAlanObjesi.SetActive(false);
                        _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._yasakliAlanObjesi.SetActive(true);
                    }
                }
                else
                {
                    _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._serbestAlanObjesi.SetActive(false);
                    _yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._yasakliAlanObjesi.SetActive(false);
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                _paraToplayici.transform.position = new Vector3(15, 0, 6);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitSoketInfo, float.MaxValue, _soketLayerMask))
                {
                    _sayac = 0;
                    for (int i = 0; i < _yakalananTurret.transform.GetChild(0).transform.childCount; i++)
                    {
                        if (_yakalananTurret.transform.GetChild(0).GetChild(i).GetComponent<SoketKontrolEtme>()._objeYerlestirilebilir)
                        {
                            _sayac++;
                        }
                    }
                    if (_sayac == _yakalananTurret.transform.GetChild(0).transform.childCount)
                    {
                        _turretinYakalandigiKonum.transform.position = new Vector3(hitSoketInfo.transform.position.x, 0.25f, hitSoketInfo.transform.position.z);
                        if (PlayerPrefs.GetInt("OnboardingDone") == 0)
                        {
                            GameObject.Find("OnboardingCotrol").GetComponent<OnboardingControl>()._devam2 = true;
                        }


                    }
                }
                else if (Physics.Raycast(ray, out RaycastHit hitMergeInfo, float.MaxValue, _mergeLayerMask))
                {
                    if (hitMergeInfo.transform.tag == "merge")
                    {
                        if (_yakalananTurret.transform.GetChild(0).GetChild(0).GetComponent<SoketKontrolEtme>()._objeYerlestirilebilir)
                        {

                            if (_yakalananTurret.transform.GetComponent<TurretMergeKontrol>()._turretNum == 2)
                            {
                                _turretinYakalandigiKonum.transform.position = new Vector3(hitMergeInfo.transform.position.x + .5f, 0, hitMergeInfo.transform.position.z);
                                hitMergeInfo.transform.GetComponent<mergeAlaniDoluluk>()._doluluk = true;
                            }
                            else
                            {
                                _turretinYakalandigiKonum.transform.position = new Vector3(hitMergeInfo.transform.position.x, .25f, hitMergeInfo.transform.position.z);
                                hitMergeInfo.transform.GetComponent<mergeAlaniDoluluk>().
                                    _doluluk = true;
                            }
                            _yakalananTurret.transform.GetComponent<TurretMergeKontrol>()._mergeEdilebilir = true;
                        }

                    }
                }
                else
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
                _yakalananTurret.transform.DOMove(_turretinYakalandigiKonum.position, 0.4f);
                _yakalananTurret = _geciciKonum;
            }
        }
    }
}
