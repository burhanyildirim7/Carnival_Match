using System;
using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;
using UnityEngine;
using UnityEngine.UI;

public class UIKontrolcu : MonoBehaviour
{
    [SerializeField] private Text _canText, _altinText, _elmasText, _yildizText;
    [SerializeField] private Text _canGeriSayimText;
    [SerializeField] private GameObject _buyCan, _buyAltin, _buyElmas;


    // Start is called before the first frame update
    void Start()
    {

        var numLives = PlayerPrefs.GetInt("num_lives");
        var maxLives = PuzzleMatchManager.instance.gameConfig.maxLives;
        _canText.text = numLives.ToString();
        _buyCan.transform.GetComponent<Button>().enabled= numLives == maxLives ? false : true;
        PuzzleMatchManager.instance.livesSystem.Subscribe(OnLivesCountdownUpdated, OnLivesCountdownFinished);



        _yildizText.text = PlayerPrefs.GetInt("HomeSceneToplamYildiz").ToString();
    }

    private void OnDestroy()
    {
        PuzzleMatchManager.instance.livesSystem.Unsubscribe(OnLivesCountdownUpdated, OnLivesCountdownFinished);
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void OnLivesCountdownUpdated(TimeSpan timeSpan, int lives)
    {
        _canGeriSayimText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        _canText.text = lives.ToString();
       // lifeImage.sprite = lives == 0 ? disabledLifeSprite : enabledLifeSprite;
        var maxLives = PuzzleMatchManager.instance.gameConfig.maxLives;
        _buyCan.transform.GetComponent<Button>().enabled= lives == maxLives ? false : true;
    }

    /// <summary>
    /// Called when the lives countdown finishes.
    /// </summary>
    /// <param name="lives">The current number of lives.</param>
    private void OnLivesCountdownFinished(int lives)
    {
        _canGeriSayimText.text = "Full";
        _canText.text = lives.ToString();
        //lifeImage.sprite = lives == 0 ? disabledLifeSprite : enabledLifeSprite;
        _buyCan.transform.GetComponent<Button>().enabled = false;
    }
}
