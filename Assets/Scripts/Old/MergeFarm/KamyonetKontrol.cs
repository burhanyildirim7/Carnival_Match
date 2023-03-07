using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class KamyonetKontrol : MonoBehaviour
{

    [SerializeField] GameObject _paraGrubu,_tikObjesi;
    [SerializeField] Animator _kamyonAnim;
    [SerializeField] Slider _paraDolulukSlideri;
    [SerializeField] TextMeshProUGUI _sliderTexti;
    [SerializeField] string _kamyonAdi;
    void Start()
    {
        if (PlayerPrefs.GetInt("KasaIlkKez")==0)
        {
            PlayerPrefs.SetInt("KasaIlkKez",1);
            PlayerPrefs.SetFloat("KasadakiParaAdeti", 0);
        }
        _kamyonAnim.SetBool("run", true);
        transform.DOLocalMove(new Vector3(0, -0.5f, 0), 1f).OnComplete(()=> _kamyonAnim.SetBool("run", false));

        _paraDolulukSlideri.value = PlayerPrefs.GetFloat("KamyonetSliderDeger");
        if (PlayerPrefs.GetFloat("KasadakiParaAdeti") > 0)
        {
            _paraDolulukSlideri.value = PlayerPrefs.GetFloat("KasadakiParaAdeti") / _paraGrubu.transform.childCount;
            _sliderTexti.text = (100*_paraDolulukSlideri.value).ToString("0.#") + " %";
        }
        for (int i = 0; i < PlayerPrefs.GetFloat("KasadakiParaAdeti"); i++)
        {
            _paraGrubu.transform.GetChild(i).transform.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        /*if (_kamyonAdi=="1")
        {
            if (PlayerPrefs.GetInt("Kamyon" + 1) == 0)
            {
                PlayerPrefs.SetInt("Kamyon" + 1, 1);
                PlayerPrefs.SetInt("Kamyon" + 0, 0);
                KamyonSifirla();
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("Kamyon" + 0) == 0)
            {
                PlayerPrefs.SetInt("Kamyon" + 0, 1);
                PlayerPrefs.SetInt("Kamyon" + 1, 0);
                KamyonSifirla();
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="para" && PlayerPrefs.GetFloat("KasadakiParaAdeti") < _paraGrubu.transform.childCount)
        {
            Debug.Log("PARA GELDİİİİ");
            _paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).gameObject.SetActive(true);
            _paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).transform.GetChild(0).gameObject.SetActive(true);
            _paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).transform.GetChild(1).gameObject.SetActive(true);
            //text
            _paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).transform.GetChild(0).GetChild(0).GetChild(0).
                transform.GetComponent<TextMeshProUGUI>().text = "$" + ((int)(PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan)).ToString();
            //move
            _paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).transform.GetChild(0).transform.DOLocalMove
                (_paraGrubu.transform.GetChild((int)PlayerPrefs.GetFloat("KasadakiParaAdeti")).transform.GetChild(1).transform.localPosition, 0.5f);

            PlayerPrefs.SetFloat("totalScore", PlayerPrefs.GetFloat("totalScore") + (PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan));
            UIController.instance.SetGamePlayScoreText();

            PlayerPrefs.SetFloat("KasadakiParaAdeti",PlayerPrefs.GetFloat("KasadakiParaAdeti") +1);
            Debug.Log(PlayerPrefs.GetFloat("KasadakiParaAdeti"));
            if (PlayerPrefs.GetFloat("KasadakiParaAdeti") > 0)
            {
                _paraDolulukSlideri.value = PlayerPrefs.GetFloat("KasadakiParaAdeti") / _paraGrubu.transform.childCount;
                _sliderTexti.text = (100*_paraDolulukSlideri.value).ToString("0.#") + " %";
            }

            if (PlayerPrefs.GetFloat("KasadakiParaAdeti") >=_paraGrubu.transform.childCount)
            {
                _kamyonAnim.SetBool("run",true);
                transform.DOLocalMove(new Vector3(18, -0.5f, 0),1f).OnComplete(()=>KamyonGeriDon());
                if (PlayerPrefs.GetInt("KamyonSirasi")==0)
                {
                    PlayerPrefs.SetInt("KamyonSirasi", 1);
                    PlayerPrefs.SetFloat("KasadakiParaAdeti", 0);
                    for (int i = 0; i < _paraGrubu.transform.childCount; i++)
                    {
                        _paraGrubu.transform.GetChild(i).transform.gameObject.SetActive(false);
                    }
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                    transform.parent.GetChild(1).transform.GetComponent<KamyonetKontrol>().KamyonSifirla();
                }
                else if (PlayerPrefs.GetInt("KamyonSirasi") == 1)
                {
                    PlayerPrefs.SetInt("KamyonSirasi", 0);
                    PlayerPrefs.SetFloat("KasadakiParaAdeti", 0);
                    for (int i = 0; i < _paraGrubu.transform.childCount; i++)
                    {
                        _paraGrubu.transform.GetChild(i).transform.gameObject.SetActive(false);
                    }
                    transform.parent.GetChild(0).gameObject.SetActive(true);
                    transform.parent.GetChild(0).transform.GetComponent<KamyonetKontrol>().KamyonSifirla();
                }
                else
                {

                }
                
            }

        }
    }


    private void KamyonGeriDon()
    {
        PlayerPrefs.SetFloat("KasadakiParaAdeti", 0);
        PlayerPrefs.SetFloat("KamyonetSliderDeger",0);
        _paraDolulukSlideri.value = 0;
        _sliderTexti.text = (100*_paraDolulukSlideri.value).ToString("0.#") + " %";
        _kamyonAnim.SetBool("run", false);
        transform.localPosition = new Vector3(-18, -0.5f, 0);
        //transform.gameObject.SetActive(false);

    }

    public void KamyonSifirla()
    {
        _kamyonAnim.SetBool("run", true);
        transform.DOLocalMove(new Vector3(0, -0.5f, 0), 1f).OnComplete(() => _kamyonAnim.SetBool("run", false));

    }

}
