// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using GameVanilla.Game.Popups;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using GameVanilla.Core;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.Common;


namespace GameVanilla.Core
{
    // This class is responsible for loading the next scene in a transition.
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] int _levelNum, _bolumSonucu;
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        //public GameBoard gameBoard;
        //public CoinsSystem coinsSystem;

        private int _kazanilacakCoinMiktari;

        /// <summary>
        /// Performs the transition to the next scene.
        /// </summary>
        public void PerformTransition()
        {
            PlayerPrefs.SetInt("OyundaKazanilanYildizSayisi", 0);
            Transition.LoadLevel(scene, duration, color);
        }
        public void OpenGameSceneK()
        {
            //PlayerPrefs.SetInt("MevcutLevel", 10);
            _levelNum = PlayerPrefs.GetInt("MevcutLevel");
            var scene = GameObject.Find("HomeScene").GetComponent<HomeScene>();
            if (!FileUtils.FileExists("Levels/" + _levelNum))
            {
                scene.OpenPopup<AlertPopup>("Popups/AlertPopup",
                    popup => popup.SetText("This level does not exist."));
            }
            else
            {
                scene.OpenPopup<StartGamePopup>("Popups/StartGamePopup", popup =>
                {
                    popup.LoadLevelData(_levelNum);
                });
            }

        }
        public void _nextButtonInGameScene()
        {
            PlayerPrefs.SetInt("HomeSceneToplamYildiz", PlayerPrefs.GetInt("HomeSceneToplamYildiz") + PlayerPrefs.GetInt("OyundaKazanilanYildizSayisi"));
            Transition.LoadLevel(scene, duration, color);
            //10-11    650  6
            _bolumSonucu = ((PlayerPrefs.GetInt("KalanLimit") * 50)) / 100;
            _kazanilacakCoinMiktari = (100 + (_bolumSonucu * 100));
            PuzzleMatchManager.instance.coinsSystem.LevelCoinEkle(_kazanilacakCoinMiktari);
        }
    }
}
