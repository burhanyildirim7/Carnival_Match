using GameVanilla.Core;
using GameVanilla.Game.Common;

using Photon.Pun;
using UnityEngine;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when a player tries to exit a game.
    /// </summary>
    public class ExitGamePopup : Popup
    {
        /// <summary>
        /// Called when the close button is pressed.
        /// </summary>
        public void OnCloseButtonPressed()
        {
            if (PlayerPrefs.GetInt("vibration_enabled") == 0)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }
            else
            {

            }
            Close();
        }

        /// <summary>
        /// Called when the exit button is pressed.
        /// </summary>
        public void OnExitButtonPressed()
        {
            if (PlayerPrefs.GetInt("vibration_enabled") == 0)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }
            else
            {

            }
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LeaveRoom();
            }
            else
            {
                PuzzleMatchManager.instance.livesSystem.RemoveLife();
            }
            GetComponent<SceneTransition>().PerformTransition();
        }

        /// <summary>
        /// Called when the resume button is pressed.
        /// </summary>
        public void OnResumeButtonPressed()
        {
            if (PlayerPrefs.GetInt("vibration_enabled") == 0)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }
            else
            {

            }
            Close();
        }
    }
}
