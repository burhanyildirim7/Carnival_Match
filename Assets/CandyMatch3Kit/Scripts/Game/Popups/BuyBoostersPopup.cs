﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup for buying boosters.
    /// </summary>
	public class BuyBoostersPopup : Popup
    {
#pragma warning disable 649
        [SerializeField]
        private Sprite lollipopSprite;

        [SerializeField]
        private Sprite bombSprite;

        [SerializeField]
        private Sprite switchSprite;

        [SerializeField]
        private Sprite colorBombSprite;

        [SerializeField]
        private Text boosterNameText;

        [SerializeField]
        private Text boosterDescriptionText;

        [SerializeField]
        private Image boosterImage;

        [SerializeField]
        private Text boosterAmountText;

        [SerializeField]
        private Text boosterCostText;

        [SerializeField]
        private Text numCoinsText;

        [SerializeField]
        private ParticleSystem coinParticles;
#pragma warning restore 649

        private BuyBoosterButton buyButton;

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(lollipopSprite);
            Assert.IsNotNull(bombSprite);
            Assert.IsNotNull(switchSprite);
            Assert.IsNotNull(colorBombSprite);
            Assert.IsNotNull(boosterNameText);
            Assert.IsNotNull(boosterDescriptionText);
            Assert.IsNotNull(boosterImage);
            Assert.IsNotNull(boosterAmountText);
            Assert.IsNotNull(boosterCostText);
            Assert.IsNotNull(numCoinsText);
            Assert.IsNotNull(coinParticles);
        }

        /// <summary>
        /// Unity's Start method.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            numCoinsText.text = PlayerPrefs.GetInt("num_coins").ToString();
        }

        /// <summary>
        /// Sets the booster button associated to this popup.
        /// </summary>
        /// <param name="button">The booster button.</param>
        public void SetBooster(BuyBoosterButton button)
        {
            buyButton = button;
            switch (button.boosterType)
            {
                case BoosterType.Lollipop:
                    boosterImage.sprite = lollipopSprite;
                    boosterNameText.text = "Hammer";
                    boosterDescriptionText.text = "Destroy an object of your choice on the board.";
                    break;

                case BoosterType.Bomb:
                    boosterImage.sprite = bombSprite;
                    boosterNameText.text = "Arrow";
                    boosterDescriptionText.text = "Destroys all objects in the selected row.";
                    break;

                case BoosterType.Switch:
                    boosterImage.sprite = switchSprite;
                    boosterNameText.text = "Anvil";
                    boosterDescriptionText.text = "Destroys all objects in the selected column.";
                    break;

                case BoosterType.ColorBomb:
                    boosterImage.sprite = colorBombSprite;
                    boosterNameText.text = "Color bomb";
                    boosterDescriptionText.text = "Creates a color bomb in the place of the selected object.";
                    break;
            }

            boosterImage.SetNativeSize();

            boosterAmountText.text = PuzzleMatchManager.instance.gameConfig.ingameBoosterAmount[buyButton.boosterType].ToString();
            boosterCostText.text = PuzzleMatchManager.instance.gameConfig.ingameBoosterCost[buyButton.boosterType].ToString();
        }

        /// <summary>
        /// Called when the buy button is pressed.
        /// </summary>
        public void OnBuyButtonPressed()
        {
            var playerPrefsKey = string.Format("num_boosters_{0}", (int)buyButton.boosterType);
            var numBoosters = PlayerPrefs.GetInt(playerPrefsKey);

            Close();

            var gameScene = parentScene as GameScene;
            if (gameScene != null)
            {
                var cost = PuzzleMatchManager.instance.gameConfig.ingameBoosterCost[buyButton.boosterType];
                var coins = PlayerPrefs.GetInt("num_coins");

                if (cost > coins)
                {
                    var scene = parentScene;
                    if (scene != null)
                    {
                        SoundManager.instance.PlaySound("Button");
                        var button = buyButton;
                        scene.OpenPopup<BuyCoinsPopup>("Popups/BuyCoinsPopup",
                            popup =>
                            {
                                popup.onClose.AddListener(
                                    () =>
                                    {
                                        scene.OpenPopup<BuyBoostersPopup>("Popups/BuyBoostersPopup",
                                            buyBoostersPopup => { buyBoostersPopup.SetBooster(button); });

                                    });
                            });
                    }
                }
                else
                {
                    PuzzleMatchManager.instance.coinsSystem.SpendCoins(cost);
                    coinParticles.Play();
                    SoundManager.instance.PlaySound("CoinsPopButton");
                    numBoosters += PuzzleMatchManager.instance.gameConfig.ingameBoosterAmount[buyButton.boosterType];
                    PlayerPrefs.SetInt(playerPrefsKey, numBoosters);
                    buyButton.UpdateAmount(numBoosters);
                }
            }
        }
    }
}
