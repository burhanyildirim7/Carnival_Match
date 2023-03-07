// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when the player wins a game.
    /// </summary>
    public class PVPWinPopup : PVPEndGamePopup
    {
#pragma warning disable 649

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
            PlayerPrefs.SetInt("MevcutLevel", PlayerPrefs.GetInt("MevcutLevel") + 1);
        }

    }
}
