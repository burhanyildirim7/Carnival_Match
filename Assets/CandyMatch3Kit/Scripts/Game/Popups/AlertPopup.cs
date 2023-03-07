// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the alert popup.
    /// </summary>
    public class AlertPopup : Popup
    {
        [SerializeField] bool _hakkattenmi;
        BaseScene _baseScene;
#pragma warning disable 649
        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Text bodyText;
#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(titleText);
            Assert.IsNotNull(bodyText);
            _baseScene = GameObject.FindObjectOfType<BaseScene>();
        }

        /// <summary>
        /// Called when the popup button is pressed.
        /// </summary>
        public void OnButtonPressed()
        {
            if (_hakkattenmi)
            {
                if (PlayerPrefs.GetInt("CreationPanel") == 1)
                {
                    PlayerPrefs.SetInt("CreationPanel", 0);
                    GameObject.Find("CreatePanel").GetComponent<CreatingTeamScript>().TeamClosingProgress();
                }
                else if (PlayerPrefs.GetInt("JoinTeamPanel") == 1)
                {
                    PlayerPrefs.SetInt("JoinTeamPanel", 0);
                    GameObject.Find("TeamPanel").GetComponent<TeamPanelScript>().JoiningClosingProgress();
                }
                else
                {

                }

            }
            else
            {

            }
            Close();
        }

        /// <summary>
        /// Called when the close button is pressed.
        /// </summary>
        public void OnCloseButtonPressed()
        {
            if (_hakkattenmi)
            {
                if (PlayerPrefs.GetInt("CreationPanel") == 1)
                {
                    _baseScene.OpenPopup<SettingsPopup>("Popups/TeamDetailPopUp");
                }
                else if (PlayerPrefs.GetInt("JoinTeamPanel") == 1)
                {
                    _baseScene.OpenPopup<SettingsPopup>("Popups/JoinTeamDetailPopUp");
                }
                else
                {

                }

            }
            else
            {

            }
            Close();
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The title text.</param>
        public void SetTitle(string text)
        {
            titleText.text = text;
        }

        /// <summary>
        /// Sets the body text.
        /// </summary>
        /// <param name="text">The body text.</param>
        public void SetText(string text)
        {
            bodyText.text = text;
        }
    }
}
