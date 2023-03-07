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

        [SerializeField]
        private Image star1;

        [SerializeField]
        private Image star2;

        [SerializeField]
        private Image star3;

       /* [SerializeField]
        private ParticleSystem star1Particles;

        [SerializeField]
        private ParticleSystem star1WhiteParticles;

        [SerializeField]
        private ParticleSystem star2Particles;

        [SerializeField]
        private ParticleSystem star2WhiteParticles;

        [SerializeField]
        private ParticleSystem star3Particles;

        [SerializeField]
        private ParticleSystem star3WhiteParticles;*/

        [SerializeField]
        private Sprite disabledStarSprite;

        [SerializeField]
        private Text _kazanilanCoinText;

        private int _kazanilanCoinMiktari;
#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(star1);
            Assert.IsNotNull(star2);
            Assert.IsNotNull(star3);
           /* Assert.IsNotNull(star1Particles);
            Assert.IsNotNull(star1WhiteParticles);
            Assert.IsNotNull(star2Particles);
            Assert.IsNotNull(star2WhiteParticles);
            Assert.IsNotNull(star3Particles);
            Assert.IsNotNull(star3WhiteParticles);*/
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
                star1.sprite = disabledStarSprite;
                star2.sprite = disabledStarSprite;
                star3.sprite = disabledStarSprite;
               /* star1Particles.gameObject.SetActive(false);
                star1WhiteParticles.gameObject.SetActive(false);
                star2Particles.gameObject.SetActive(false);
                star2WhiteParticles.gameObject.SetActive(false);
                star3Particles.gameObject.SetActive(false);
                star3WhiteParticles.gameObject.SetActive(false);*/
            }
            else if (stars == 1)
            {
                star2.sprite = disabledStarSprite;
                star3.sprite = disabledStarSprite;
               /* star2Particles.gameObject.SetActive(false);
                star2WhiteParticles.gameObject.SetActive(false);
                star3Particles.gameObject.SetActive(false);
                star3WhiteParticles.gameObject.SetActive(false);*/
            }
            else if (stars == 2)
            {
                star3.sprite = disabledStarSprite;
               /* star3Particles.gameObject.SetActive(false);
                star3WhiteParticles.gameObject.SetActive(false);*/
            }

            //GameWinSetCoins(gameBoard._kalanLimit);
        }

        public void GameWinSetCoins(int limit)
        {
            _bolumSonucu = (limit * 50) / 100;
            _kazanilanCoinMiktari = 100 + (_bolumSonucu*100);
            _kazanilanCoinText.text = _kazanilanCoinMiktari.ToString();
        }
    }
}
