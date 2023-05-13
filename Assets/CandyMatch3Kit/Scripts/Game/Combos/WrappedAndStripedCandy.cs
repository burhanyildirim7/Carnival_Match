using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;
using UnityEngine;

public class WrappedAndStripedCandy : MonoBehaviour
{
    [SerializeField] private GameObject _dikeyRoketler;
    [SerializeField] private GameObject _yatayRoketler;

    private List<GameObject> cachedTiles = new List<GameObject>();
    private List<GameObject> cachedTiles2 = new List<GameObject>();
    private List<GameObject> cachedTiles3 = new List<GameObject>();
    private List<GameObject> cachedTiles4 = new List<GameObject>();
    private List<GameObject> cachedTiles5 = new List<GameObject>();
    private List<GameObject> cachedTiles6 = new List<GameObject>();

    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> tiles2 = new List<GameObject>();
    private List<GameObject> tiles3 = new List<GameObject>();
    private List<GameObject> tiles4 = new List<GameObject>();
    private List<GameObject> tiles5 = new List<GameObject>();
    private List<GameObject> tiles6 = new List<GameObject>();

    private int _sayi1;
    private int _sayi2;

    private int _sayi3;
    private int _sayi4;

    private int _sayi5;
    private int _sayi6;

    private int _sayi7;
    private int _sayi8;

    private int _sayi9;
    private int _sayi10;

    private int _sayi11;
    private int _sayi12;

    private GameBoard _board;

    public void Resolve(GameBoard board, Tile tileA, Tile tileB)
    {
        var x = tileB.x;
        var y = tileB.y;

        GameObject roket2Konum = board.GetTile(x + 1, y);
        GameObject roket3Konum = board.GetTile(x - 1, y);

        GameObject roket4Konum = board.GetTile(x, y + 1);
        GameObject roket5Konum = board.GetTile(x, y - 1);


        board.ComboRoketlePatlat(tileA.gameObject);
        board.ComboRoketlePatlat(tileB.gameObject);

        /*

        if (board.GetTile(x + 1, y).GetComponent<ColorBomb>() != null)
        {
            _board.ColorBombPatlat(board.GetTile(x + 1, y));
        }
        else if (board.GetTile(x + 1, y).GetComponent<StripedCandy>() != null)
        {
            _board.RoketlePatlat(board.GetTile(x + 1, y));
        }
        else
        {
            _board.BoosterIlePatlat(board.GetTile(x + 1, y));
        }

        if (board.GetTile(x - 1, y).GetComponent<ColorBomb>() != null)
        {
            _board.ColorBombPatlat(board.GetTile(x - 1, y));
        }
        else if (board.GetTile(x - 1, y).GetComponent<StripedCandy>() != null)
        {
            _board.RoketlePatlat(board.GetTile(x - 1, y));
        }
        else
        {
            _board.BoosterIlePatlat(board.GetTile(x - 1, y));
        }

        if (board.GetTile(x, y + 1).GetComponent<ColorBomb>() != null)
        {
            _board.ColorBombPatlat(board.GetTile(x, y + 1));
        }
        else if (board.GetTile(x, y + 1).GetComponent<StripedCandy>() != null)
        {
            _board.RoketlePatlat(board.GetTile(x, y + 1));
        }
        else
        {
            _board.BoosterIlePatlat(board.GetTile(x, y + 1));
        }

        if (board.GetTile(x, y - 1).GetComponent<ColorBomb>() != null)
        {
            _board.ColorBombPatlat(board.GetTile(x, y - 1));
        }
        else if (board.GetTile(x, y - 1).GetComponent<StripedCandy>() != null)
        {
            _board.RoketlePatlat(board.GetTile(x, y - 1));
        }
        else
        {
            _board.BoosterIlePatlat(board.GetTile(x, y - 1));
        }

        */



        _board = board;

        _sayi1 = 0;
        _sayi2 = 0;

        _sayi3 = 0;
        _sayi4 = 0;

        _sayi5 = 0;
        _sayi6 = 0;

        _sayi7 = 0;
        _sayi8 = 0;

        _sayi9 = 0;
        _sayi10 = 0;

        _sayi11 = 0;
        _sayi12 = 0;

        tiles.Clear();
        tiles2.Clear();
        tiles3.Clear();
        tiles4.Clear();
        tiles5.Clear();
        tiles6.Clear();



        /// 1. ROKET ---------------------------------------

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

        /// 2. ROKET ---------------------------------------

        _sayi3 = x - 1;
        _sayi4 = x + 1;

        for (var i = 0; i < board.level.width; i++)
        {
            var tilee = board.GetTile(i, (y - 1));
            if (tilee != null)
            {
                if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                {

                }
                else
                {
                    tiles2.Add(tilee);
                }
            }
            else
            {

            }


        }

        /// 3. ROKET ---------------------------------------

        _sayi5 = x - 1;
        _sayi6 = x + 1;

        for (var i = 0; i < board.level.width; i++)
        {
            var tilee = board.GetTile(i, (y + 1));
            if (tilee != null)
            {
                if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                {

                }
                else
                {
                    tiles3.Add(tilee);
                }
            }
            else
            {

            }


        }

        /// 4. ROKET ---------------------------------------

        _sayi7 = y - 1;
        _sayi8 = y + 1;

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
                    tiles4.Add(tilee);
                }
            }
            else
            {

            }


        }

        /// 5. ROKET ---------------------------------------

        _sayi9 = y - 1;
        _sayi10 = y + 1;

        for (var i = 0; i < board.level.height; i++)
        {
            var tilee = board.GetTile((x - 1), i);
            if (tilee != null)
            {
                if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                {

                }
                else
                {
                    tiles5.Add(tilee);
                }
            }
            else
            {

            }


        }

        /// 6. ROKET ---------------------------------------

        _sayi11 = y - 1;
        _sayi12 = y + 1;

        for (var i = 0; i < board.level.height; i++)
        {
            var tilee = board.GetTile((x + 1), i);
            if (tilee != null)
            {
                if (tilee.GetComponent<Collectable>() != null && tilee.GetComponent<Unbreakable>() != null)
                {

                }
                else
                {
                    tiles6.Add(tilee);
                }
            }
            else
            {

            }


        }


        /// 1. ROKET DEVAM ---------------------------------------

        cachedTiles.Clear();
        foreach (var tiled in tiles)
        {
            if (tiled != null)
            {
                cachedTiles.Add(tiled);
            }
        }

        /// 2. ROKET DEVAM ---------------------------------------

        cachedTiles2.Clear();
        foreach (var tiled in tiles2)
        {
            if (tiled != null)
            {
                cachedTiles2.Add(tiled);
            }
        }

        /// 3. ROKET DEVAM ---------------------------------------

        cachedTiles3.Clear();
        foreach (var tiled in tiles3)
        {
            if (tiled != null)
            {
                cachedTiles3.Add(tiled);
            }
        }

        /// 4. ROKET DEVAM ---------------------------------------

        cachedTiles4.Clear();
        foreach (var tiled in tiles4)
        {
            if (tiled != null)
            {
                cachedTiles4.Add(tiled);
            }
        }

        /// 5. ROKET DEVAM ---------------------------------------

        cachedTiles5.Clear();
        foreach (var tiled in tiles5)
        {
            if (tiled != null)
            {
                cachedTiles5.Add(tiled);
            }
        }

        /// 6. ROKET DEVAM ---------------------------------------

        cachedTiles6.Clear();
        foreach (var tiled in tiles6)
        {
            if (tiled != null)
            {
                cachedTiles6.Add(tiled);
            }
        }





        Invoke("PatlatInvoke", 0.01f);



        DikeyRoketleriOlustur(tileB.gameObject);
        DikeyRoketleriOlustur(roket2Konum);
        DikeyRoketleriOlustur(roket3Konum);


        YatayRoketleriOlustur(tileB.gameObject);
        YatayRoketleriOlustur(roket4Konum);
        YatayRoketleriOlustur(roket5Konum);

        //Debug.Log("Debug -- 4 --");
    }

    private void PatlatInvoke()
    {

        /// 1. ROKET DEVAM ---------------------------------------

        if (_sayi1 >= 0 && _sayi1 < cachedTiles.Count)
        {
            if (cachedTiles[_sayi1] != null)
            {
                if (cachedTiles[_sayi1].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles[_sayi1]);
                }
                else if (cachedTiles[_sayi1].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles[_sayi1]);
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
                else if (cachedTiles[_sayi2].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles[_sayi2]);
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

        /// 2. ROKET DEVAM ---------------------------------------

        if (_sayi3 >= 0 && _sayi3 < cachedTiles2.Count)
        {
            if (cachedTiles2[_sayi3] != null)
            {
                if (cachedTiles2[_sayi3].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles2[_sayi3]);
                }
                else if (cachedTiles2[_sayi3].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles2[_sayi3]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles2[_sayi3]);
                }

                _sayi3--;
            }
            else
            {

            }


        }
        else
        {

        }

        if (_sayi4 < cachedTiles2.Count)
        {
            if (cachedTiles2[_sayi4] != null)
            {
                if (cachedTiles2[_sayi4].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles2[_sayi4]);
                }
                else if (cachedTiles2[_sayi4].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles2[_sayi4]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles2[_sayi4]);
                }

                _sayi4++;

            }
            else
            {

            }
        }
        else
        {

        }

        /// 3. ROKET DEVAM ---------------------------------------

        if (_sayi5 >= 0 && _sayi5 < cachedTiles3.Count)
        {
            if (cachedTiles3[_sayi5] != null)
            {
                if (cachedTiles3[_sayi5].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles3[_sayi5]);
                }
                else if (cachedTiles3[_sayi5].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles3[_sayi5]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles3[_sayi5]);
                }

                _sayi5--;
            }
            else
            {

            }


        }
        else
        {

        }

        if (_sayi6 < cachedTiles3.Count)
        {
            if (cachedTiles3[_sayi6] != null)
            {
                if (cachedTiles3[_sayi6].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles3[_sayi6]);
                }
                else if (cachedTiles3[_sayi6].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles3[_sayi6]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles3[_sayi6]);
                }

                _sayi6++;

            }
            else
            {

            }
        }
        else
        {

        }

        /// 4. ROKET DEVAM ---------------------------------------

        if (_sayi7 >= 0 && _sayi7 < cachedTiles4.Count)
        {
            if (cachedTiles4[_sayi7] != null)
            {
                if (cachedTiles4[_sayi7].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles4[_sayi7]);
                }
                else if (cachedTiles4[_sayi7].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles4[_sayi7]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles4[_sayi7]);
                }

                _sayi7--;
            }
            else
            {

            }


        }
        else
        {

        }

        if (_sayi8 < cachedTiles4.Count)
        {
            if (cachedTiles4[_sayi8] != null)
            {
                if (cachedTiles4[_sayi8].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles4[_sayi8]);
                }
                else if (cachedTiles4[_sayi8].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles4[_sayi8]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles4[_sayi8]);
                }

                _sayi8++;

            }
            else
            {

            }
        }
        else
        {

        }

        /// 5. ROKET DEVAM ---------------------------------------

        if (_sayi9 >= 0 && _sayi9 < cachedTiles5.Count)
        {
            if (cachedTiles5[_sayi9] != null)
            {
                if (cachedTiles5[_sayi9].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles5[_sayi9]);
                }
                else if (cachedTiles5[_sayi9].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles5[_sayi9]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles5[_sayi9]);
                }

                _sayi9--;
            }
            else
            {

            }


        }
        else
        {

        }

        if (_sayi10 < cachedTiles5.Count)
        {
            if (cachedTiles5[_sayi10] != null)
            {
                if (cachedTiles5[_sayi10].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles5[_sayi10]);
                }
                else if (cachedTiles5[_sayi10].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles5[_sayi10]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles5[_sayi10]);
                }

                _sayi10++;

            }
            else
            {

            }
        }
        else
        {

        }

        /// 6. ROKET DEVAM ---------------------------------------

        if (_sayi11 >= 0 && _sayi11 < cachedTiles6.Count)
        {
            if (cachedTiles6[_sayi11] != null)
            {
                if (cachedTiles6[_sayi11].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles6[_sayi11]);
                }
                else if (cachedTiles6[_sayi11].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles6[_sayi11]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles6[_sayi11]);
                }

                _sayi11--;
            }
            else
            {

            }


        }
        else
        {

        }

        if (_sayi12 < cachedTiles6.Count)
        {
            if (cachedTiles6[_sayi12] != null)
            {
                if (cachedTiles6[_sayi12].GetComponent<ColorBomb>() != null)
                {
                    _board.ColorBombPatlat(cachedTiles6[_sayi12]);
                }
                else if (cachedTiles6[_sayi12].GetComponent<StripedCandy>() != null)
                {
                    _board.RoketlePatlat(cachedTiles6[_sayi12]);
                }
                else
                {
                    _board.BoosterIlePatlat(cachedTiles6[_sayi12]);
                }

                _sayi12++;

            }
            else
            {

            }
        }
        else
        {

        }

        GravityKontrol();
    }

    private void TekrarlaInvoke()
    {
        Invoke("PatlatInvoke", 0.1f);
    }

    private void GravityKontrol()
    {
        if (_sayi12 == cachedTiles6.Count)
        {
            if (_sayi11 == -1)
            {
                Invoke("GravityCalistir", 0.1f);
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

    private void GravityCalistir()
    {
        _board.ApplyGravity();
    }

    public void ComboRoketleriOlustur(GameObject objectKonum)
    {
        DikeyRoketleriOlustur(objectKonum);
        YatayRoketleriOlustur(objectKonum);
    }

    private void DikeyRoketleriOlustur(GameObject objectKonum)
    {
        var obj = Instantiate(_dikeyRoketler);
        obj.transform.position = objectKonum.transform.position;

    }

    private void YatayRoketleriOlustur(GameObject objectKonum)
    {
        var obj = Instantiate(_yatayRoketler);
        obj.transform.position = objectKonum.transform.position;

    }
}
