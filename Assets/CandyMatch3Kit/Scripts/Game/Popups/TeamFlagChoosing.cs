
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    public class TeamFlagChoosing : Popup
    {
        private Popup loadingPopup;
        BaseScene _baseScene;
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            base.Start();
            _baseScene = GameObject.FindObjectOfType<BaseScene>();

        }

        private void OnDestroy()
        {
            PuzzleMatchManager.instance.coinsSystem.Unsubscribe(OnCoinsChanged);
        }

        public void OnBuyButtonPressed(int numCoins)
        {
            PuzzleMatchManager.instance.coinsSystem.BuyCoins(numCoins);
        }

        public void OnCloseButtonPressed()
        {
            Close();
        }

        private void OnCoinsChanged(int numCoins)
        {
            GetComponent<PlaySound>().Play("CoinsPopButton");
        }


        public void OpenLoadingPopup()
        {
            #if UNITY_IOS
            parentScene.OpenPopup<LoadingPopup>("Popups/LoadingPopup",
                popup =>
                {
                    loadingPopup = popup;
                }, false);
            #endif
        }


        public void CloseLoadingPopup()
        {
            #if UNITY_IOS
            if (loadingPopup != null)
            {
                loadingPopup.Close();
            }
            #endif
        }

        public void EditButton()
        {
            _baseScene.OpenPopup<SettingsPopup>("Popups/TeamDuzenlePopUp");
        }

    }
}
