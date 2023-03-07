
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    public class EditTeamScript : Popup
    {
        BaseScene _baseScene;
        private Popup loadingPopup;
        [SerializeField] Text _teamName, _teamType, _minLeveltext;
        [SerializeField] InputField _teamDescription;
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            /*  
             PlayerPrefs.SetString("TeamName", _teamName.text);
             PlayerPrefs.SetString("TeamDescription", _teamDescription.text);
             PlayerPrefs.SetString("TeamType", _teamType.text);
             PlayerPrefs.SetString("MinLevelText", _minLevel.text);
            */
            base.Start();
            _baseScene = GameObject.FindObjectOfType<BaseScene>();
            _teamName.text = PlayerPrefs.GetString("TeamName");
            _teamDescription.text = PlayerPrefs.GetString("TeamDescription");
            _teamType.text = PlayerPrefs.GetString("TeamType");
            _minLeveltext.text = PlayerPrefs.GetString("MinLevelText");


        }
        public void SaveButton()
        {

            if (_teamName.text != "" && _teamDescription.text != "")
            {
                PlayerPrefs.SetInt("CreationPanel", 1);
                PlayerPrefs.SetString("TeamName", _teamName.text);
                PlayerPrefs.SetString("TeamDescription", _teamDescription.text);
                PlayerPrefs.SetString("TeamType", _teamType.text);
                PlayerPrefs.SetString("MinLevelText", _minLeveltext.text);
                _teamName.text = PlayerPrefs.GetString("TeamName");
                _teamDescription.text = PlayerPrefs.GetString("TeamDescription");
                _teamType.text = PlayerPrefs.GetString("TeamType");
                _minLeveltext.text = PlayerPrefs.GetString("MinLevelText");
                _baseScene.OpenPopup<SettingsPopup>("Popups/TeamDetailPopUp");
                OnCloseButtonPressed();
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

        public void TeamFlagChooseButton()
        {

            _baseScene.OpenPopup<SettingsPopup>("Popups/TeamFlagChoosePopUp");

        }





    }
}
