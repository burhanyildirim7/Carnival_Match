using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that shows the goals of a level.
    /// </summary>
    public class RakipSirasiPopup : Popup
    {
#pragma warning disable 649

        [SerializeField]
        private Image background;


#pragma warning restore 649

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(AutoKill());
            background.rectTransform.sizeDelta = new Vector2(Screen.width, 601);

        }

        private IEnumerator AutoKill()
        {
            yield return new WaitForSeconds(2.4f);
            Close();
            var gameScene = parentScene as GameScene;
            if (gameScene != null)
            {
                gameScene.StartGame();
            }
        }

    }
}
