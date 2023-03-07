using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeScript : MonoBehaviour
{
    [SerializeField] Text _fireRateText, _incomeText,_fireRateBedel,_incomeBedel;
    [SerializeField] GameObject _upgradePanel;

    void Start()
    {

        if (PlayerPrefs.GetInt("UpgradeIlkSefer")==0)
        {
            PlayerPrefs.SetFloat("EnemySpawnRate",3f);
            PlayerPrefs.SetFloat("FireRate", 1.5f);
            PlayerPrefs.SetFloat("Income", 10);

            PlayerPrefs.SetInt("FireRateLevel", 1);
            PlayerPrefs.SetInt("IncomeLevel", 1);

            PlayerPrefs.SetInt("FireRateBedel", PlayerPrefs.GetInt("FireRateBedel") + 100);
            PlayerPrefs.SetFloat("IncomeBedel", PlayerPrefs.GetFloat("IncomeBedel") + 100);

            PlayerPrefs.SetInt("UpgradeIlkSefer", 1);
        }
        else
        {
            _fireRateText.text = "LEVEL" + PlayerPrefs.GetInt("FireRateLevel").ToString();
            _incomeText.text = "+$" + (PlayerPrefs.GetFloat("Income") - 10).ToString();
            _incomeBedel.text = "$" + (PlayerPrefs.GetFloat("IncomeBedel"));

            /*if (PlayerPrefs.GetInt("IncomeBedel")<1000)
            {
                _incomeBedel.text = "$" + (PlayerPrefs.GetInt("IncomeBedel"));

            }
            else
            {
                _incomeBedel.text = "$" + ((PlayerPrefs.GetInt("IncomeBedel")/1000).ToString("0.##")+" K");

            }*/
            _fireRateBedel.text = "$" + (PlayerPrefs.GetInt("FireRateBedel"));
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("totalScore") < PlayerPrefs.GetInt("FireRateBedel"))
        {
            _fireRateText.transform.parent.transform.GetComponent<Button>().interactable = false;

        }
        else
        {
            _fireRateText.transform.parent.transform.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetFloat("totalScore") < PlayerPrefs.GetFloat("IncomeBedel"))
        {
            _incomeText.transform.parent.transform.GetComponent<Button>().interactable = false;

        }
        else
        {
            _incomeText.transform.parent.transform.GetComponent<Button>().interactable = true;
        }
    }

    public void FireRateUpgrade()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        PlayerPrefs.SetFloat("FireRate", PlayerPrefs.GetFloat("FireRate")*.95f);
        PlayerPrefs.SetInt("FireRateLevel", PlayerPrefs.GetInt("FireRateLevel")+1);
        _fireRateText.text = "LEVEL"+PlayerPrefs.GetInt("FireRateLevel").ToString();

        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - PlayerPrefs.GetInt("FireRateBedel"));
        UIController.instance.SetGamePlayScoreText();

        PlayerPrefs.SetInt("FireRateBedel", PlayerPrefs.GetInt("FireRateBedel") + (PlayerPrefs.GetInt("FireRateLevel") -1)*100 + 100);
        _fireRateBedel.text = "$" + (PlayerPrefs.GetInt("FireRateBedel"));
    }

    public void IncomeUpgrade()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        PlayerPrefs.SetFloat("Income", PlayerPrefs.GetFloat("Income") + 1);
        PlayerPrefs.SetInt("IncomeLevel", PlayerPrefs.GetInt("IncomeLevel")+1);
        _incomeText.text = "+$" + (PlayerPrefs.GetFloat("Income")-10).ToString();

        PlayerPrefs.SetFloat("totalScore", PlayerPrefs.GetFloat("totalScore") - PlayerPrefs.GetFloat("IncomeBedel"));
        UIController.instance.SetGamePlayScoreText();

        PlayerPrefs.SetFloat("IncomeBedel", PlayerPrefs.GetFloat("IncomeBedel") + +(PlayerPrefs.GetInt("IncomeLevel") - 1) * 100 + 100);
        _incomeBedel.text = "$" + (PlayerPrefs.GetFloat("IncomeBedel"));
        /*if (PlayerPrefs.GetInt("IncomeBedel") < 1000)
        {
            _incomeBedel.text = "$" + (PlayerPrefs.GetInt("IncomeBedel"));

        }
        else
        {
            _incomeBedel.text = "$" + ((PlayerPrefs.GetInt("IncomeBedel") / 1000).ToString("0.##") + " K");

        }*/
    }
    public void CloseWindowButton()
    {

        _upgradePanel.transform.gameObject.SetActive(false);

    }
}
