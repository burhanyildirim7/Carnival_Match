// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
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
    public class SwitchBooster : MonoBehaviour
    {

        private List<GameObject> tiles = new List<GameObject>();

        private GameBoard _board;

        //MonoBehaviour monoScript;

        private List<GameObject> cachedTiles = new List<GameObject>();

        private int _sayi1;

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
        public void Resolve(GameBoard board, GameObject tile)
        {
            //monoScript = GameObject.FindObjectOfType<MonoBehaviour>();


            //Debug.Log("CALISTI");
            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;
            //board.ExplodeTile(tile);



            _board = board;

            _board.BoosterModaGir();

            tiles.Clear();
            for (var i = 0; i < board.level.height; i++)
            {
                var tilee = board.GetTile(x, i);
                //Debug.Log(tilee);
                if (tilee != null)
                {
                    if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                    {

                    }
                    else
                    {
                        tiles.Add(tilee);
                    }
                }
                else
                {

                }


            }


            cachedTiles.Clear();
            foreach (var tiled in tiles)
            {
                if (tiled != null)
                {
                    cachedTiles.Add(tiled);
                }
            }


            float degerx = tile.transform.position.x;
            float degery = 100;
            //float degery = ((board.level.height) * (board.level.height) + 30);
            //float degery = ((cachedTiles.Count) * (cachedTiles.Count) + 30);

            OrsScript.instance.OrsYerineGec(degerx, degery);

            _sayi1 = 0;
            Invoke("PatlatInvoke", 0.5f);

            //monoScript.StartCoroutine(Patlat());

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

        private void PatlatInvoke()
        {
            //Debug.Log("DeniyozPatlatCalisiyo " + _sayi1);
            if (cachedTiles[_sayi1] != null)
            {
                if (cachedTiles[_sayi1].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles[_sayi1]);
                    //_board.BoosterIlePatlat(cachedTiles[_sayi1]);
                }
                else if (cachedTiles[_sayi1].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles[_sayi1]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles[_sayi1]);

                }

                _sayi1++;
            }



            if (_sayi1 == cachedTiles.Count)
            {
                _board.ApplyGravity();
                _board.BoosterModdanCik();
            }
            else
            {
                TekrarlaInvoke();
            }
        }

        private void TekrarlaInvoke()
        {
            //Debug.Log("DeniyozTekrarlaCalisiyo");
            Invoke("PatlatInvoke", 0.1f);
        }

    }
}
