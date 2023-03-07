// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Advertisements;

using GameVanilla.Game.Common;
/*
namespace GameVanilla.Core
{
    /// <summary>
    /// This system is responsible for initializing Unity Ads at startup time.
    /// </summary>
    public class AdsSystem : MonoBehaviour, IUnityAdsInitializationListener
    {
        private void Start()
        {
	        if (Application.platform != RuntimePlatform.IPhonePlayer &&
                Application.platform != RuntimePlatform.Android)
            {
                return;
            }

            InitializeAds();
        }

        public void InitializeAds()
        {
			var gameConfig = PuzzleMatchManager.instance.gameConfig;
            var gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? gameConfig.adsGameIdIos
                : gameConfig.adsGameIdAndroid;

			Advertisement.Initialize(gameId, gameConfig.adsTestMode, this);
        }

        public void OnInitializationComplete()
        {
            //Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            //Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
*/