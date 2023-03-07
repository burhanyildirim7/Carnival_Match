using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class moneyToplamaScript : MonoBehaviour
{
    [SerializeField] GameObject _paraBlastFX, _paraTextCanvas, _ucusHedefObjesi, _parentObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "toplayici")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            if (PlayerPrefs.GetInt("SesKapat") == 0)
            {
                transform.GetComponent<AudioSource>().enabled = true;
                transform.GetComponent<AudioSource>().Play();
            }
            else
            {
                transform.GetComponent<AudioSource>().enabled = true;
                transform.GetComponent<AudioSource>().Play();
                //transform.GetComponent<AudioSource>().enabled = false;
            }
            transform.GetComponent<BoxCollider>().enabled = false;
            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + (int)(PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan));
            UIController.instance.SetGamePlayScoreText();
            _paraTextCanvas.SetActive(true);
            _paraTextCanvas.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "$" + ((int)(PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan)).ToString();
            _paraTextCanvas.transform.DOLocalMove(_ucusHedefObjesi.transform.localPosition, .5f);
            Instantiate(_paraBlastFX, null).transform.position = transform.position;
            transform.DOScale(500, .3f);
            transform.DOJump(transform.position, 1, 1, .5f);
            Invoke("paraGeriDonme", .55f);
        }
    }

    private void paraGeriDonme()
    {
        _parentObject.transform.parent.GetComponent<EnemySpawnerScript>()._moneyStackParent.RemoveAt(0);
        transform.parent = _parentObject.transform;
        transform.parent.parent.GetComponent<EnemySpawnerScript>()._moneyList.Add(transform.gameObject);
        transform.localPosition = Vector3.zero;
        _paraTextCanvas.transform.localPosition = Vector3.zero;
        _paraTextCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OtoToplanma()
    {
        transform.GetComponent<AudioSource>().Play();
        transform.GetComponent<BoxCollider>().enabled = false;
        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + (int)(PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan));
        UIController.instance.SetGamePlayScoreText();
        _paraTextCanvas.SetActive(true);
        _paraTextCanvas.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "$" + ((int)(PlayerPrefs.GetFloat("Income") * GameObject.Find("KATSAYI_PARENT").GetComponent<KatsayiHesaplama>()._toplamCarpan)).ToString();
        _paraTextCanvas.transform.DOLocalMove(_ucusHedefObjesi.transform.localPosition, .5f);
        Instantiate(_paraBlastFX, null).transform.position = transform.position;
        transform.DOScale(500, .3f);
        transform.DOJump(transform.position, 1, 1, .5f);
        Invoke("paraGeriDonme", .55f);
    }
}
