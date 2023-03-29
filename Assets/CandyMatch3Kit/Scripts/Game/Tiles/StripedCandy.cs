// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;

using GameVanilla.Core;
using System.Collections;
using UnityEditor;

namespace GameVanilla.Game.Common
{
    public enum StripeDirection
    {
        Horizontal,
        Vertical
    }

    /// <summary>
    /// The class used for striped candies.
    /// </summary>
    public class StripedCandy : Candy
    {
        public StripeDirection direction;

        private readonly List<GameObject> cachedTiles = new List<GameObject>();

        private List<GameObject> tiles = new List<GameObject>();

        private GameBoard _board;

        MonoBehaviour monoScript;

        private int _sayi1;

        /// <summary>
        /// Returns a list containing all the tiles destroyed when this tile explodes.
        /// </summary>
        /// <returns>A list containing all the tiles destroyed when this tile explodes.</returns>
        /*
        public override List<GameObject> Explode()
        {
            var tiles = new List<GameObject>();

            if (direction == StripeDirection.Horizontal)
            {
                for (var i = 0; i < board.level.width; i++)
                {
                    if (board.GetTile(i, y) != null)
                    {
                        var tile = board.GetTile(i, y);
                        if (tile.GetComponent<Tile>() != null)
                        {
                            tiles.Add(tile);
                        }
                        //tiles.Add(tile);
                    }
                    //var tile = board.GetTile(i, y);
                    //tiles.Add(tile);
                }
            }
            else
            {
                for (var j = 0; j < board.level.height; j++)
                {
                    if (board.GetTile(x, j) != null)
                    {
                        var tile = board.GetTile(x, j);
                        if (tile.GetComponent<Tile>() != null)
                        {
                            tiles.Add(tile);
                        }
                        //tiles.Add(tile);
                    }
                    //var tile = board.GetTile(x, j);
                    //tiles.Add(tile);
                }
            }

            cachedTiles.Clear();
            foreach (var tile in tiles)
            {
                if (tile != null)
                {
                    cachedTiles.Add(tile);
                }
            }

            return tiles;
        }
        */

        public override List<GameObject> Explode()
        {
            return new List<GameObject> { gameObject };
        }

        public void Resolve(GameBoard board, GameObject tile)
        {
            monoScript = GameObject.FindObjectOfType<MonoBehaviour>();


            //Debug.Log("CALISTI");
            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;
            //board.ExplodeTile(tile);

            //Debug.Log(tile);

            _board = board;

            //_board.BoosterIlePatlat(tile);

            tiles.Clear();

            if (direction == StripeDirection.Horizontal)
            {
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
            }
            else
            {
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
            }



            cachedTiles.Clear();
            foreach (var tiled in tiles)
            {
                if (tiled != null)
                {
                    cachedTiles.Add(tiled);
                }
            }


            //float degerx = ((cachedTiles.Count) * (-cachedTiles.Count) - 20);
            //float degerx = ((board.level.width) * (-board.level.width) - 20);
            float degerx = -110;
            float degery = tile.transform.position.y;

            //ArrowScript.instance.ArrowYerineGec(degerx, degery);
            Invoke("PatlatInvoke", 0.05f);
            //monoScript.StartCoroutine(Patlat());
            //monoScript.StartCoroutine(Deneme());

        }

        private IEnumerator Patlat()
        {

            //yield return new WaitForSeconds(0.5f);

            int deg1 = 0;
            int deg2 = 0;

            for (int i = 0; i < 20; i++)
            {
                deg1++;
                Debug.Log("Deger 1 :" + deg1);

            }

            for (var i = 0; i < cachedTiles.Count; i++)
            {
                Debug.Log("Element " + i + ":" + cachedTiles[i].activeSelf);
            }

            for (var i = 0; i < cachedTiles.Count; i++)
            {
                Debug.Log("Elementtttt " + i + ":" + cachedTiles[i].activeSelf);

                if (cachedTiles[i] != null)
                {
                    if (cachedTiles[i].GetComponent<ColorBomb>() != null)
                    {
                        _board.ColorBombPatlat(cachedTiles[i]);
                        _board.BoosterIlePatlat(cachedTiles[i]);
                    }
                    else if (cachedTiles[i].GetComponent<StripedCandy>() != null && cachedTiles[i] != gameObject)
                    {
                        _board.RoketlePatlat(cachedTiles[i]);
                    }
                    else
                    {
                        _board.BoosterIlePatlat(cachedTiles[i]);
                    }

                    yield return new WaitForSeconds(0.05f);
                }
            }

            for (int i = 0; i < 20; i++)
            {
                deg2++;
                Debug.Log("Deger 2 :" + deg2);
            }

            //yield return new WaitForSeconds(0.1f);

            _board.ApplyGravity();

            //_board.BoosterModdanCik();

            //Debug.Log("BITTI");

        }

        private void PatlatInvoke()
        {
            //Debug.Log("DeniyozPatlatCalisiyo");
            if (cachedTiles[_sayi1] != null)
            {
                if (cachedTiles[_sayi1].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles[_sayi1]);
                    _board.BoosterIlePatlat(cachedTiles[_sayi1]);
                }
                else if (cachedTiles[_sayi1].GetComponent<StripedCandy>() != null && cachedTiles[_sayi1] != gameObject)
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
            }
            else
            {
                TekrarlaInvoke();
            }
        }

        private void TekrarlaInvoke()
        {
            //Debug.Log("DeniyozTekrarlaCalisiyo");
            Invoke("PatlatInvoke", 0.05f);
        }



        /// <summary>
        /// Shows the visual effects associated to the explosion of this tile.
        /// </summary>
        /// <param name="pool">The pool to use for the visual effects.</param>

        public override void ShowExplosionFx(FxPool pool)
        {
            /*
            base.ShowExplosionFx(pool);

            foreach (var tile in cachedTiles)
            {
                if (tile != null)
                {
                    var stripes = pool.GetStripedCandyExplosionPool(direction).GetObject();
                    stripes.transform.position = tile.transform.position;
                }
            }
            */

            SoundManager.instance.PlaySound("LineVerticalHorizontal");
        }

    }
}
