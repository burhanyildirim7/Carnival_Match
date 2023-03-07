// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using FullSerializer;

using GameVanilla.Core;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;


namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class used for the color bomb + candy combo.
    /// </summary>
    public class ColorBombWithCandyCombo : ColorBombCombo
    {

        //MonoBehaviour monoScript;
        /// <summary>
        /// Resolves this combo.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="tiles">The tiles destroyed by the combo.</param>
        /// <param name="fxPool">The pool to use for the visual effects.</param>
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            //monoScript = GameObject.FindObjectOfType<MonoBehaviour>();
            //monoScript.StartCoroutine(Delay(board, tiles, fxPool));

            base.Resolve(board, tiles, fxPool);

            var candy = tileA.GetComponent<Candy>() != null ? tileA : tileB;



            for (var i = tiles.Count - 1; i >= 0; i--)
            {
                var tile = tiles[i];
                if (tile != null && tile.GetComponent<Candy>() != null &&
                    tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
                {
                    board.ExplodeTileNonRecursive(tile);
                }

            }

            SoundManager.instance.PlaySound("ColorBomb");

            board.ApplyGravity();

        }

        /*
        private IEnumerator Delay(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            yield return new WaitForSeconds(0.1f);
            base.Resolve(board, tiles, fxPool);

            var candy = tileA.GetComponent<Candy>() != null ? tileA : tileB;



            for (var i = tiles.Count - 1; i >= 0; i--)
            {
                var tile = tiles[i];
                if (tile != null && tile.GetComponent<Candy>() != null &&
                    tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
                {
                    board.ExplodeTileNonRecursive(tile);
                }

            }

            SoundManager.instance.PlaySound("ColorBomb");

            board.ApplyGravity();

        }
        */
    }
}
