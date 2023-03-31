// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Game.Common;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when the player wins a game.
    /// </summary>
    public class WinPopup : EndGamePopup
    {
        int _bolumSonucu;
#pragma warning disable 649

        public GameBoard gameBoard;

        int _earnMagnetAmount;


        [SerializeField]
        private Sprite disabledStarSprite;


#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(disabledStarSprite);
        }
        protected override void Start()
        {
            PlayerPrefs.SetInt("MevcutLevel", PlayerPrefs.GetInt("MevcutLevel") + 1);
        }
        /// <summary>
        /// Sets the number of stars obtained in the level.
        /// </summary>
        /// <param name="stars">The number of stars obtained in the level.</param>
        public void SetStars(int stars)
        {
            if (stars == 0)
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
            PlayerPrefs.SetInt("LeveldenKazanilanMagnetMiktari", _earnMagnetAmount);
        }

        public void GameWinSetCoins(int limit)
        {
        }
    }
}
