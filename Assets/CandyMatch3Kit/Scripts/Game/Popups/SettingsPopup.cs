// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Scenes;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the settings popup.
    /// </summary>
    public class SettingsPopup : Popup
    {
        [SerializeField] GameObject _soundOnImg, _soundOffImg,
            _musicOnImg, _musicOffImg,
            _vibrationOnImg,_vibrationOffImg;
#pragma warning disable 649

#pragma warning restore 649

        private int currentSound, currentMusic, currentVibration, currentNotifications;


        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Unity's Start method.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            if (PlayerPrefs.GetInt("ilk_acilis_ses_ayari_kontrol") == 1)
            {   

            }
            else
            {
                PlayerPrefs.SetInt("music_enabled",1);
                currentSound = PlayerPrefs.GetInt("sound_enabled");
                PlayerPrefs.SetInt("sound_enabled", 1);
                currentMusic = PlayerPrefs.GetInt("music_enabled");
                PlayerPrefs.SetInt("vibration_enabled", 1);
                currentVibration = PlayerPrefs.GetInt("vibration_enabled");

                _musicOnImg.SetActive(true);
                _musicOffImg.SetActive(false);

                _soundOnImg.SetActive(true);
                _soundOffImg.SetActive(false);

                _vibrationOnImg.SetActive(true);
                _vibrationOffImg.SetActive(false);

                PlayerPrefs.SetInt("ilk_acilis_ses_ayari_kontrol", 1);
            }

            if (PlayerPrefs.GetInt("music_enabled") == 1)
            {
                _musicOnImg.SetActive(true);
                _musicOffImg.SetActive(false);
            }
            else
            {
                _musicOnImg.SetActive(false);
                _musicOffImg.SetActive(true);
            }


            if (PlayerPrefs.GetInt("sound_enabled") == 1)
            {
                _soundOnImg.SetActive(true);
                _soundOffImg.SetActive(false);
            }
            else
            {
                _soundOnImg.SetActive(false);
                _soundOffImg.SetActive(true);
            }

            if (PlayerPrefs.GetInt("vibration_enabled") == 1)
            {
                _vibrationOnImg.SetActive(true);
                _vibrationOffImg.SetActive(false);
            }
            else
            {
                _vibrationOnImg.SetActive(false);
                _vibrationOffImg.SetActive(true);
            }


        }

        /// <summary>
        /// Called when the close button is pressed.
        /// </summary>
        public void OnCloseButtonPressed()
        {
            Close();
        }

        /// <summary>
        /// Called when the save button is pressed.
        /// </summary>
        public void OnSaveButtonPressed()
        {
            SoundManager.instance.SetSoundEnabled(currentSound == 1);
            SoundManager.instance.SetMusicEnabled(currentMusic == 1);
            var homeScene = parentScene as HomeScene;
            if (homeScene != null)
            {
                homeScene.UpdateButtons();
            }
            Close();
        }

        /// <summary>
        /// Called when the reset progress button is pressed.
        /// </summary>
        public void OnResetProgressButtonPressed()
        {
            PuzzleMatchManager.instance.lastSelectedLevel = 0;
            PlayerPrefs.SetInt("next_level", 0);
            for (var i = 1; i <= 30; i++)
            {
                PlayerPrefs.DeleteKey(string.Format("level_stars_{0}", i));
            }
        }

        /// <summary>
        /// Called when the help button is pressed.
        /// </summary>
        public void OnHelpButtonPressed()
        {
            parentScene.OpenPopup<AlertPopup>("Popups/AlertPopup", popup =>
            {
                popup.SetTitle("Help");
                popup.SetText("Do you need help? \n info@lumiergames.com");
            }, false);
        }

        /// <summary>
        /// Called when the info button is pressed.
        /// </summary>
        public void OnInfoButtonPressed()
        {
            parentScene.OpenPopup<AlertPopup>("Popups/AlertPopup", popup =>
            {
                popup.SetTitle("About");
                popup.SetText("Created by Lumier Games.\n Copyright (C) 2023.");
            }, false);
        }

        /// <summary>
        /// Called when the girl avatar is selected.
        /// </summary>
        public void OnGirlAvatarSelected()
        {
        }

        /// <summary>
        /// Called when the boy avatar is selected.
        /// </summary>
        public void OnBoyAvatarSelected()
        {
        }

        /// <summary>
        /// Called when the sound slider value is changed.
        /// </summary>
        public void OnSoundSliderValueChanged()
        {
            if (PlayerPrefs.GetInt("sound_enabled")==0)
            {
                PlayerPrefs.SetInt("sound_enabled", 1);
                currentSound = PlayerPrefs.GetInt("sound_enabled");
                _soundOnImg.SetActive(true);
                _soundOffImg.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("sound_enabled", 0);
                currentSound = PlayerPrefs.GetInt("sound_enabled");
                _soundOnImg.SetActive(false);
                _soundOffImg.SetActive(true);
            }
            SoundManager.instance.SetSoundEnabled(currentSound == 1);
            SoundManager.instance.SetMusicEnabled(currentMusic == 1);
            var homeScene = parentScene as HomeScene;
            if (homeScene != null)
            {
                homeScene.UpdateButtons();
            }
        }

        /// <summary>
        /// Called when the music slider value is changed.
        /// </summary>
        public void OnMusicSliderValueChanged()
        {
            if (PlayerPrefs.GetInt("music_enabled") == 0)
            {
                PlayerPrefs.SetInt("music_enabled", 1);
                currentMusic = PlayerPrefs.GetInt("music_enabled");
                _musicOnImg.SetActive(true);
                _musicOffImg.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("music_enabled", 0);
                currentMusic = PlayerPrefs.GetInt("music_enabled");
                _musicOnImg.SetActive(false);
                _musicOffImg.SetActive(true);
            }
            SoundManager.instance.SetSoundEnabled(currentSound == 1);
            SoundManager.instance.SetMusicEnabled(currentMusic == 1);
            var homeScene = parentScene as HomeScene;
            if (homeScene != null)
            {
                homeScene.UpdateButtons();
            }
        }
        public void VibrationValueChanged()
        {
            if (PlayerPrefs.GetInt("vibration_enabled") == 0)
            {
                PlayerPrefs.SetInt("vibration_enabled", 1);
                currentVibration = PlayerPrefs.GetInt("vibration_enabled");
                _vibrationOnImg.SetActive(true);
                _vibrationOffImg.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("vibration_enabled", 0);
                currentVibration = PlayerPrefs.GetInt("vibration_enabled");
                _vibrationOnImg.SetActive(false);
                _vibrationOffImg.SetActive(true);
            }
        }
    }
}
