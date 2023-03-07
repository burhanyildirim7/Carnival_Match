using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using DG.Tweening;

public class StickmanAnimation : MonoBehaviour
{
    Animator _stickmanAnimator;

    [SerializeField] Slider _canBari2, _canBari5;
    [SerializeField] GameObject _moneyObject, _splashObject, _vurulmaFX, _parentObject;
    [SerializeField] Material _griMat, _kirmiziMat;
    [SerializeField] List<GameObject> _moneyPoint = new List<GameObject>();
    private GameObject _tempMoney;
    public bool _isboss, _dur;
    private float _hiz;
    public Slider _canBari;

    public bool _secildi; // kullanmayacağım ama belki kullanırız diye var. boss geldiginde sadece tek taret kilitlenmesine sebep olur görüntü güzel olmaz. 

    private bool _ilkVurulma;
    // Start is called before the first frame update
    void Start()
    {
        _stickmanAnimator = transform.GetComponent<Animator>();
        if (_isboss)
        {
            _isboss = true;
            transform.localScale = new Vector3(2, 2, 2);
            _canBari5.gameObject.transform.parent.gameObject.SetActive(true);
            _canBari2.gameObject.transform.parent.gameObject.SetActive(false);
            _canBari = _canBari5;
            _stickmanAnimator.SetBool("run", false);
            _stickmanAnimator.SetBool("slowRun", true);
            _stickmanAnimator.SetBool("injuredRun", false);
            _stickmanAnimator.SetBool("die", false);
            _stickmanAnimator.SetBool("attack", false);
            _moneyPoint[0].transform.parent.transform.localScale = new Vector3(.5f, .5f, .5f);
            _hiz = 1;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            _canBari5.gameObject.transform.parent.gameObject.SetActive(false);
            _canBari2.gameObject.transform.parent.gameObject.SetActive(true);
            _canBari = _canBari2;
            //int _random = Random.Range(0,2);

            _stickmanAnimator.SetBool("run", true);
            _stickmanAnimator.SetBool("slowRun", false);
            _stickmanAnimator.SetBool("injuredRun", false);
            _stickmanAnimator.SetBool("die", false);
            _stickmanAnimator.SetBool("attack", false);

            _hiz = 1.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            if (GameObject.Find("SOKETLER_PARENT").transform.GetComponent<AnaSoketKontrol>()._SYSTEMCONTROL)
            {
                if (_dur)
                {
                }
                else
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * _hiz);
                    _stickmanAnimator.speed = 1;
                }
            }
            else
            {
                _stickmanAnimator.speed = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "warZone")
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        }

        if (other.tag == "projectile")
        {
            if (PlayerPrefs.GetInt("SesKapat") == 0)
            {
                transform.GetComponent<AudioSource>().Play();
            }
            else
            {

            }
            Destroy(other.gameObject);
            if (_canBari.value > 0f)
            {
                _canBari.value = _canBari.value - 1;
                if (_ilkVurulma)
                {
                    _vurulmaFX.transform.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    _ilkVurulma = true;
                    _vurulmaFX.gameObject.SetActive(true);
                }
            }

            if (_isboss)
            {
                if (_canBari.value == 4)
                {
                    _stickmanAnimator.SetBool("run", false);
                    _stickmanAnimator.SetBool("slowRun", false);
                    _stickmanAnimator.SetBool("injuredRun", true);
                    _stickmanAnimator.SetBool("die", false);
                    _stickmanAnimator.SetBool("attack", false);

                }

            }
            else
            {
                if (_canBari.value == 1)
                {
                    _stickmanAnimator.SetBool("run", false);
                    _stickmanAnimator.SetBool("slowRun", false);
                    _stickmanAnimator.SetBool("injuredRun", true);
                    _stickmanAnimator.SetBool("die", false);
                    _stickmanAnimator.SetBool("attack", false);

                }

            }


            if (_canBari.value <= 0)
            {
                //transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                transform.GetComponent<BoxCollider>().enabled = false;
                _stickmanAnimator.SetBool("run", false);
                _stickmanAnimator.SetBool("slowRun", false);
                _stickmanAnimator.SetBool("injuredRun", false);
                _stickmanAnimator.SetBool("die", true);
                _stickmanAnimator.SetBool("attack", false);
                _dur = true;
                transform.GetComponent<BoxCollider>().enabled = false;
                if (_isboss)
                {
                    for (int i = 0; i < _moneyPoint.Count; i++)
                    {
                        /*
                        _tempMoney = Instantiate(_moneyObject, transform);
                        _tempMoney.transform.parent = null;
                        _tempMoney.transform.localScale = new Vector3(350, 350, 350);
                        _tempMoney.transform.DOLocalJump(_moneyPoint[i].transform.position, 1, 1, .5f);
                        */

                        _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList[0].gameObject.SetActive(true);
                        _tempMoney = _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList[0].gameObject;
                        _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList.RemoveAt(0);
                        _tempMoney.transform.parent = transform;
                        _tempMoney.transform.GetComponent<BoxCollider>().enabled = true;
                        _tempMoney.transform.localPosition = new Vector3(0, .25f, 0);
                        _tempMoney.transform.parent = null;
                        _tempMoney.transform.localScale = new Vector3(350, 350, 350);
                        _tempMoney.transform.DOLocalJump(_moneyPoint[i].transform.position, 1, 1, .5f);
                        _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyStackParent.Add(_tempMoney.gameObject);

                    }
                }
                else
                {/*
                    _tempMoney = Instantiate(_moneyObject, transform);
                    _tempMoney.transform.parent = null;
                    _tempMoney.transform.localScale = new Vector3(350, 350, 350);
                    _tempMoney.transform.DOLocalJump(_moneyPoint[0].transform.position, 1, 1, .5f);
                    */
                    _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList[0].gameObject.SetActive(true);
                    _tempMoney = _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList[0].gameObject;
                    _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyList.RemoveAt(0);
                    _tempMoney.transform.parent = transform;
                    _tempMoney.transform.GetComponent<BoxCollider>().enabled = true;
                    _tempMoney.transform.localPosition = new Vector3(0, .25f, 0);
                    _tempMoney.transform.parent = null;
                    _tempMoney.transform.localScale = new Vector3(350, 350, 350);
                    _tempMoney.transform.DOLocalJump(_moneyPoint[0].transform.position, 1, 1, .5f);
                    _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyStackParent.Add(_tempMoney.gameObject);

                }
                _canBari.gameObject.transform.parent.transform.gameObject.SetActive(false);
                transform.GetChild(1).GetChild(2).transform.GetComponent<Renderer>().material = _griMat;
                _splashObject.gameObject.SetActive(true);
                _splashObject.transform.DOScale(new Vector3(0, 0, 0), 0.01f).OnComplete(() =>
                            _splashObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f)).OnComplete(() =>
                            _splashObject.transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f));


                PlayerPrefs.SetInt("OldurulenDusmanSayisi", PlayerPrefs.GetInt("OldurulenDusmanSayisi") + 1);
                //SDK icindeki level takip kodu buraya yazılacak

                //AppMetrica.Instance.ReportEvent("oldurulen_dusman_sayisi - " + PlayerPrefs.GetInt("OldurulenDusmanSayisi").ToString());
                //AppMetrica.Instance.SendEventsBuffer();

                Invoke("_karakteriGeriAl", 3f);
                //Destroy(gameObject, 3f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
            _stickmanAnimator.SetBool("run", false);
            _stickmanAnimator.SetBool("slowRun", false);
            _stickmanAnimator.SetBool("injuredRun", false);
            _stickmanAnimator.SetBool("die", false);
            _stickmanAnimator.SetBool("attack", true);
            _dur = true;
        }
    }

    private void _karakteriGeriAl()
    {
        transform.parent = _parentObject.transform;
        _splashObject.gameObject.SetActive(false);
        //_dur = false;
        if (_isboss)
        {
            transform.parent.parent.GetComponent<EnemySpawnerScript>()._bossList.Add(transform.gameObject);
            _canBari5.value = 10;
            _canBari2.value = 2;
            _canBari.value = 10;
            _stickmanAnimator.SetBool("run", false);
            _stickmanAnimator.SetBool("slowRun", true);
            _stickmanAnimator.SetBool("injuredRun", false);
            _stickmanAnimator.SetBool("die", false);
            _stickmanAnimator.SetBool("attack", false);

            _canBari.gameObject.transform.parent.transform.gameObject.SetActive(true);

        }
        else
        {
            transform.parent.parent.GetComponent<EnemySpawnerScript>()._enemyList.Add(transform.gameObject);
            _canBari5.value = 10;
            _canBari2.value = 2;
            _canBari.value = 2;
            _stickmanAnimator.SetBool("run", true);
            _stickmanAnimator.SetBool("slowRun", false);
            _stickmanAnimator.SetBool("injuredRun", false);
            _stickmanAnimator.SetBool("die", false);
            _stickmanAnimator.SetBool("attack", false);

            _canBari.gameObject.transform.parent.transform.gameObject.SetActive(true);

        }
        //transform.localPosition = Vector3.zero;
        transform.DOLocalMove(Vector3.zero, 0.1f);
        transform.GetChild(1).GetChild(2).transform.GetComponent<Renderer>().material = _kirmiziMat;

        _secildi = false;

        Invoke("Kapat", 0.2f);

    }

    private void Kapat()
    {
        gameObject.SetActive(false);
    }
}
