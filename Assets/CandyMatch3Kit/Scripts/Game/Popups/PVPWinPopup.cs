using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Game.Common;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when the player wins a game.
    /// </summary>
    public class PvPWinPopup : PvPEndGamePopup
    {
        int _bolumSonucu;
#pragma warning disable 649

        public GameBoard gameBoard;

        int _earnMagnetAmount;




#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            //PlayerPrefs.SetInt("MevcutLevel", PlayerPrefs.GetInt("MevcutLevel") + 1);
        }
        /// <summary>
        /// Sets the number of stars obtained in the level.
        /// </summary>
        /// <param name="stars">The number of stars obtained in the level.</param>
        public void SetStars(int stars)
        {
            /*if (stars == 0)
            {
                _earnMagnetAmount = 0;
            }
            else if (stars == 1)
            {
                _earnMagnetAmount = 1;
            }
            else if (stars == 2)
            {
                _earnMagnetAmount = 2;
            }
            else
            {
                _earnMagnetAmount = 3;
            }
            PlayerPrefs.SetInt("LeveldenKazanilanMagnetMiktari", _earnMagnetAmount);*/
        }

        public void GameWinSetCoins(int limit)
        {
        }
    }
}
