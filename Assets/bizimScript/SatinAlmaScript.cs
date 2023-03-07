using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;
using GameVanilla.Game.UI;


namespace GameVanilla.Game.Scenes
{

    public class SatinAlmaScript : BaseScene
    {

        [SerializeField]
        private Text numCoinsText;


        // Start is called before the first frame update
        private void Start()
        {
            var numCoins = PlayerPrefs.GetInt("num_coins");
            //Debug.Log("SAHIP OLDUGUM PARA:"+numCoins);
            if (numCoins >= 1000)
            {
                numCoinsText.text = (numCoins / 1000f).ToString("F1") + "K";
            }
            else if (numCoins >= 1000000)
            {
                numCoinsText.text = (numCoins / 1000000f).ToString("F1") + "M";
            }
            else if (numCoins >= 1000000000)
            {
                numCoinsText.text = (numCoins / 1000000000f).ToString("F1") + "B";
            }
            else
            {
                numCoinsText.text = numCoins.ToString("F1");
            }

            PuzzleMatchManager.instance.coinsSystem.Subscribe(OnCoinsChanged);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDestroy()
        {
            PuzzleMatchManager.instance.coinsSystem.Unsubscribe(OnCoinsChanged);

        }

        public void OnSpinWheelButtonPressed()
        {
            OpenPopup<SpinWheelPopup>("Popups/SpinWheelPopup", popup =>
            {
                var gameConfig = PuzzleMatchManager.instance.gameConfig;
                popup.SetInfo(gameConfig.spinWheelItems, gameConfig.spinWheelCost);
            });
        }
        public void OnBuyCoinButtonPressed()
        {
            OpenPopup<BuyCoinsPopup>("Popups/BuyCoinsPopup");
        }

        /// <summary>
        /// Called when the number of coins changes.
        /// </summary>
        /// <param name="numCoins">The current number of coins.</param>
        private void OnCoinsChanged(int numCoins)
        {
            if (numCoins >= 1000)
            {
                numCoinsText.text = (numCoins / 1000f).ToString("F1") + "K";
            }
            else if (numCoins >= 1000000)
            {
                numCoinsText.text = (numCoins / 1000000f).ToString("F1") + "M";
            }
            else if (numCoins >= 1000000000)
            {
                numCoinsText.text = (numCoins / 1000000000f).ToString("F1") + "B";
            }
            else
            {
                numCoinsText.text = numCoins.ToString("F1");
            }
            Debug.Log("SAHIP OLDUGUM PARA:" + numCoins);

        }

        public void OnBuyLivesButtonPressed()
        {
            var numLives = PlayerPrefs.GetInt("num_lives");
            var maxLives = PuzzleMatchManager.instance.gameConfig.maxLives;
            if (numLives < maxLives)
            {
                OpenPopup<BuyLivesPopup>("Popups/BuyLivesPopup");
            }
        }

    }

}
