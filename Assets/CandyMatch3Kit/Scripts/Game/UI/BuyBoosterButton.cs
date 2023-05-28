using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;

using Photon.Pun;

namespace GameVanilla.Game.UI
{
	/// <summary>
	/// This class represents the in-game button that is used to buy a booster.
	/// </summary>
	public class BuyBoosterButton : MonoBehaviour
	{
		public BoosterType boosterType;

#pragma warning disable 649
		[SerializeField]
		private GameScene gameScene;

		[SerializeField]
		public GameObject amountGroup;

		[SerializeField]
		public GameObject moreGroup;

		[SerializeField]
		private Text amountText;

#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        private void Awake()
		{
            Assert.IsNotNull(gameScene);
            if (PhotonNetwork.IsConnected)
			{

			}
			else
			{
                Assert.IsNotNull(amountGroup);
                Assert.IsNotNull(moreGroup);
                Assert.IsNotNull(amountText);
            }
        }

		/// <summary>
		/// Called when the button is pressed.
		/// </summary>
		public void OnButtonPressed()
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            if (gameScene.gameBoard.CurrentlyAwarding)
			{
				return;
			}

			var playerPrefsKey = string.Format("num_boosters_{0}", (int)boosterType);
			var numBoosters = PlayerPrefs.GetInt(playerPrefsKey);
			if (numBoosters == 0)
			{
				gameScene.OpenPopup<BuyBoostersPopup>("Popups/BuyBoostersPopup", popup => { popup.SetBooster(this); });

            }
            else
			{
				gameScene.EnableBoosterMode(this);
            }
        }

		public void PVPSkillButtonPressed()
		{
            gameScene.EnableBoosterMode(this);
        }

        /// <summary>
        /// Updates the amount of boosters of the button.
        /// </summary>
        /// <param name="amount">The amount of boosters.</param>
        public void UpdateAmount(int amount)
		{
			if (amount == 0)
			{
				amountGroup.SetActive(false);
				moreGroup.SetActive(true);
			}
			else
			{
				amountGroup.SetActive(true);
				moreGroup.SetActive(false);
				amountText.text = amount.ToString();
			}
		}
	}
}
