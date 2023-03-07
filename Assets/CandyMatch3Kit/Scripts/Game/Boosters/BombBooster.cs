﻿// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class that represents the bomb booster.
    /// </summary>
    public class BombBooster : Booster
    {

        private List<GameObject> tiles = new List<GameObject>();

        private GameBoard _board;

        MonoBehaviour monoScript;

        //private readonly List<GameObject> cachedTiles = new List<GameObject>();
        /// <summary>
        /// Resolves this booster.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="tile">The tile in which to apply the booster.</param>
        /// 
        /*
		public override void Resolve(GameBoard board, GameObject tile)
		{
            var tiles = new List<GameObject>();
			var x = tile.GetComponent<Tile>().x;
			var y = tile.GetComponent<Tile>().y;
            tiles.Add(board.GetTile(x - 1, y - 1));
            tiles.Add(board.GetTile(x, y - 1));
            tiles.Add(board.GetTile(x + 1, y - 1));
            tiles.Add(board.GetTile(x - 1, y));
            tiles.Add(tile);
            tiles.Add(board.GetTile(x + 1, y));
            tiles.Add(board.GetTile(x - 1, y + 1));
            tiles.Add(board.GetTile(x, y + 1));
            tiles.Add(board.GetTile(x + 1, y + 1));

			foreach (var t in tiles)
			{
				if (t != null && t.GetComponent<Tile>().destructable)
				{
					board.ExplodeTileViaBooster(t);
				}
			}
		}
		*/
        /*
        public override void Resolve(GameBoard board, GameObject tile)
        {
            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;
            board.ExplodeTile(tile);
            board.CreateVerticalStripedTileBooster(x, y, CandyColor.Blue);
        }
        */
        public override void Resolve(GameBoard board, GameObject tile)
        {
            monoScript = GameObject.FindObjectOfType<MonoBehaviour>();


            //Debug.Log("CALISTI");
            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;
            //board.ExplodeTile(tile);

            Debug.Log(tile);

            _board = board;

            tiles.Clear();
            for (var i = 0; i < board.level.width; i++)
            {
                var tilee = board.GetTile(i, y);
                //Debug.Log(tilee);
                if (tilee != null)
                {
                    if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                    {

                    }
                    else
                    {
                        tiles.Add(tilee);
                        //Debug.Log(tilee);
                        //Debug.Log("################################################");
                    }
                }
                else
                {

                }

            }

            /*
            cachedTiles.Clear();
            foreach (var tiled in tiles)
            {
                if (tiled != null)
                {
                    cachedTiles.Add(tiled);
                }
            }
            */

            //float degerx = ((cachedTiles.Count) * (-cachedTiles.Count) - 20);
            //float degerx = ((board.level.width) * (-board.level.width) - 20);
            float degerx = -110;
            float degery = tile.transform.position.y;

            ArrowScript.instance.ArrowYerineGec(degerx, degery);

            monoScript.StartCoroutine(Patlat());

        }

        private IEnumerator Patlat()
        {

            yield return new WaitForSeconds(0.5f);

            for (var i = 0; i < tiles.Count; i++)
            {
                if (tiles[i] != null)
                {
                    if (tiles[i].GetComponent<ColorBomb>() != null)
                    {
                        _board.ColorBombPatlat(tiles[i]);
                        _board.BoosterIlePatlat(tiles[i]);
                    }
                    else
                    {
                        _board.BoosterIlePatlat(tiles[i]);

                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }

            //yield return new WaitForSeconds(0.1f);

            _board.ApplyGravity();

            _board.BoosterModdanCik();

            //Debug.Log("BITTI");

        }

    }
}
