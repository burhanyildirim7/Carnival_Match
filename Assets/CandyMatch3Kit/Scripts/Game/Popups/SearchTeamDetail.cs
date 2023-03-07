
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    public class SearchTeamDetail: Popup
    {
        private Popup loadingPopup;
        BaseScene _baseScene;
        [SerializeField] Text _teamName, _teamDescription, _teamType, _minLeveltext,_teamCapacity;
        [SerializeField] GameObject _content;
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            base.Start();
            _baseScene = GameObject.FindObjectOfType<BaseScene>();
            _teamName.text = PlayerPrefs.GetString("SearchTeamName");
            _teamDescription.text = PlayerPrefs.GetString("SearchTeamDescription");
            _teamType.text = PlayerPrefs.GetString("SearchTeamType");
            _minLeveltext.text = PlayerPrefs.GetString("SearchMinLevelText");
            _teamCapacity.text= _content.GetComponent<ContentDizme>()._contentAdeti.ToString()+"/50";

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

        public void JoinButton()
        {
            
            GameObject.Find("TeamPanel").GetComponent<TeamPanelScript>().JoiningProgress();
            OnCloseButtonPressed();
        }
    }
}
