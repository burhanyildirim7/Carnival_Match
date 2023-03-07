using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class BinaAnimasyon : MonoBehaviour
{
    Animator _animator ;
    [SerializeField] GameObject _bosAlan;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo) == 0)
        {
            gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo) == 1)
        {
            _animator.SetBool("run", true);
            PlayerPrefs.SetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo, 2);
            _bosAlan.GetComponent<Image>().enabled = false;
            _bosAlan.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            _bosAlan.GetComponent<Image>().enabled = false;
            _bosAlan.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo) == 0)
        {
            gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo) == 1)
        {
            _animator.SetBool("run", true);
            PlayerPrefs.SetInt("Acildimi" + _bosAlan.GetComponent<BinaKurma>()._haritaNo + transform.parent.GetComponent<BinaKurma>()._binaNo, 2);
            _bosAlan.GetComponent<Image>().enabled = false;
            _bosAlan.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {

        }
    }

}
