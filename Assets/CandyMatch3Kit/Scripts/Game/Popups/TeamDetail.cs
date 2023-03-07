
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    public class TeamDetail : Popup
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
            if (PlayerPrefs.GetInt("CreationPanel") == 1)
            {
                _teamName.text = PlayerPrefs.GetString("TeamName");
                _teamDescription.text = PlayerPrefs.GetString("TeamDescription");
                _teamType.text = PlayerPrefs.GetString("TeamType");
                _minLeveltext.text = PlayerPrefs.GetString("MinLevelText");
                _teamCapacity.text = _content.GetComponent<ContentDizme>()._contentAdeti.ToString() + "/50";
            }
            else if (PlayerPrefs.GetInt("JoinTeamPanel") == 1)
            {
                _teamName.text = PlayerPrefs.GetString("SearchTeamName");
                _teamDescription.text = PlayerPrefs.GetString("SearchTeamDescription");
                _teamType.text = PlayerPrefs.GetString("SearchTeamType");
                _minLeveltext.text = PlayerPrefs.GetString("SearchMinLevelText");
                _teamCapacity.text = _content.GetComponent<ContentDizme>()._contentAdeti.ToString() + "/50";
            }
            else
            {

            }

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
            OnCloseButtonPressed();
        }
        public void TeamClosing()
        {
            _baseScene.OpenPopup<SettingsPopup>("Popups/HakkettenmiPopUp");



            OnCloseButtonPressed();
        }
        public void JoinTeamClosing()
        {
            _baseScene.OpenPopup<SettingsPopup>("Popups/HakkettenmiPopUp");

            OnCloseButtonPressed();
        }
    }
}
