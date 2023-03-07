using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Popups;
using UnityEngine;
using UnityEngine.UI;
using GameVanilla.Core;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.Common;


// This class is responsible for loading the next scene in a transition.
public class PVPSceneTransition : MonoBehaviour
{
    [SerializeField] int _levelNum;
    public string scene = "<Insert scene name>";
    public float duration = 1.0f;
    public Color color = Color.black;

    [SerializeField] private Text _priceText;

    int coins;
    int _cost;

    /// <summary>
    /// Performs the transition to the next scene.
    /// </summary>
    public void PerformTransition()
    {
        PlayerPrefs.SetInt("OyundaKazanilanYildizSayisi", 0);
        Transition.LoadLevel(scene, duration, color);
    }

    public void OpenPVPSceneK()
    {
        _levelNum = 99990;

        _cost = int.Parse(_priceText.text);
        coins = PlayerPrefs.GetInt("num_coins");

        var scene = GameObject.Find("HomeScene").GetComponent<HomeScene>();

        if (_cost <= coins)
        {
            PuzzleMatchManager.instance.coinsSystem.SpendCoins(_cost);
            SoundManager.instance.PlaySound("CoinsPopButton");

            if (!FileUtils.FileExists("Levels/" + _levelNum))
            {
                scene.OpenPopup<AlertPopup>("Popups/AlertPopup",
                    popup => popup.SetText("This level does not exist."));
            }
            else
            {
                scene.OpenPopup<StartPVPPopup>("Popups/StartPVPPopup", popup =>
                {
                    popup.LoadLevelData(_levelNum);
                });
            }

        }
        else
        {
            Debug.Log("YETERLİ PARA YOK");
            Debug.Log("cost: " + _cost);
            Debug.Log("coins: " + coins);
        }



    }

    public void _nextButtonInGameScene()
    {
        PlayerPrefs.SetInt("HomeSceneToplamYildiz", PlayerPrefs.GetInt("HomeSceneToplamYildiz") + PlayerPrefs.GetInt("OyundaKazanilanYildizSayisi"));
        Transition.LoadLevel(scene, duration, color);
    }
}

