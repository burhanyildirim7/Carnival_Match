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

    public class StripedCandy : Candy
    {
        public StripeDirection direction;

        private List<GameObject> cachedTiles = new List<GameObject>();

        private List<GameObject> tiles = new List<GameObject>();

        private GameBoard _board;

        MonoBehaviour monoScript;

        private int _sayi1;

        private int _sayi2;

        private int _bulunduguSira;

        [SerializeField] private GameObject _dikeyRoketler;
        [SerializeField] private GameObject _yatayRoketler;

        public bool _patladim;

        private bool _patlamaBitti;

        public override List<GameObject> Explode()
        {
            _patladim = true;
            return new List<GameObject> { gameObject };
        }

        public void Resolve(GameBoard board, GameObject tile)
        {
            //GameBoard._patlamaSirasi++;

            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;

            _board = board;

            _bulunduguSira = 0;
            _sayi1 = 0;
            _sayi2 = 0;

            tiles.Clear();

            if (direction == StripeDirection.Horizontal)
            {

                _bulunduguSira = x;
                _sayi1 = x - 1;
                _sayi2 = x + 1;

                for (var i = 0; i < board.level.width; i++)
                {
                    var tilee = board.GetTile(i, y);

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
            else
            {

                _bulunduguSira = y;
                _sayi1 = y - 1;
                _sayi2 = y + 1;

                for (var i = 0; i < board.level.height; i++)
                {
                    var tilee = board.GetTile(x, i);

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



            Invoke("PatlatInvoke", 0.01f);

            if (direction == StripeDirection.Horizontal)
            {
                YatayRoketleriOlustur();
            }
            else
            {
                DikeyRoketleriOlustur();
            }
        }

        private void PatlatInvoke()
        {
            if (_sayi1 >= 0 && _sayi1 < cachedTiles.Count)
            {
                Debug.Log("Sayı 1  =  " + _sayi1);
                Debug.Log("CachedTile Sayı  =  " + cachedTiles.Count);
                if (cachedTiles[_sayi1] != null)
                {
                    if (cachedTiles[_sayi1].GetComponent<ColorBomb>() != null)
                    {
                        _board.ColorBombPatlat(cachedTiles[_sayi1]);
                    }
                    else if (cachedTiles[_sayi1].GetComponent<StripedCandy>() != null && cachedTiles[_sayi1] != gameObject)
                    {
                        if (cachedTiles[_sayi1].GetComponent<StripedCandy>()._patladim == true)
                        {

                        }
                        else
                        {
                            _board.RoketlePatlat(cachedTiles[_sayi1]);
                        }
                    }
                    else
                    {
                        _board.BoosterIlePatlat(cachedTiles[_sayi1]);
                    }

                    _sayi1--;
                }
                else
                {

                }


            }
            else
            {

            }

            if (_sayi2 < cachedTiles.Count)
            {
                if (cachedTiles[_sayi2] != null)
                {
                    if (cachedTiles[_sayi2].GetComponent<ColorBomb>() != null)
                    {
                        _board.ColorBombPatlat(cachedTiles[_sayi2]);
                    }
                    else if (cachedTiles[_sayi2].GetComponent<StripedCandy>() != null && cachedTiles[_sayi2] != gameObject)
                    {
                        if (cachedTiles[_sayi2].GetComponent<StripedCandy>()._patladim == true)
                        {

                        }
                        else
                        {
                            _board.RoketlePatlat(cachedTiles[_sayi2]);
                        }

                    }
                    else
                    {
                        _board.BoosterIlePatlat(cachedTiles[_sayi2]);
                    }

                    _sayi2++;

                }
                else
                {

                }
            }
            else
            {

            }

            if (_patlamaBitti == false)
            {
                GravityKontrol();
            }
            else
            {

            }

        }

        private void GravityKontrol()
        {
            //Debug.Log("patlama Sırası = " + GameBoard._patlamaSirasi);
            if (_sayi2 == cachedTiles.Count)
            {
                //Debug.Log("Sayı 2 Kontrol");
                if (_sayi1 == -1)
                {


                    if (_board._colorBombAktif == false)
                    {
                        _board.ApplyGravity();
                    }
                    else
                    {

                    }



                }
                else
                {
                    TekrarlaInvoke();
                }

            }
            else
            {
                TekrarlaInvoke();
            }
        }

        private void TekrarlaInvoke()
        {
            Invoke("PatlatInvoke", 0.1f);
        }

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

        private void DikeyRoketleriOlustur()
        {
            var obj = Instantiate(_dikeyRoketler);
            obj.transform.position = gameObject.transform.position;
        }

        private void YatayRoketleriOlustur()
        {
            var obj = Instantiate(_yatayRoketler);
            obj.transform.position = gameObject.transform.position;
        }

    }
}
