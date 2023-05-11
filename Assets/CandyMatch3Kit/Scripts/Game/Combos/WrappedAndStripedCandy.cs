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

    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> tiles2 = new List<GameObject>();

    private int _sayi1;

    private int _sayi2;

    private int _sayi3;

    private int _sayi4;

    private GameBoard _board;

    public void Resolve(GameBoard board, Tile tileA, Tile tileB)
    {

        var x = tileB.x;
        var y = tileB.y;

        var a = tileA.x;
        var b = tileA.y;

        var tilesToExplode = new List<GameObject>();

        //ExplodeRow(board, fxPool, tilesToExplode, y - 1);
        //ExplodeRow(board, fxPool, tilesToExplode, y);
        //ExplodeRow(board, fxPool, tilesToExplode, y + 1);
        //ExplodeColumn(board, fxPool, tilesToExplode, x - 1);
        //ExplodeColumn(board, fxPool, tilesToExplode, x);
        //ExplodeColumn(board, fxPool, tilesToExplode, x + 1);

        board.GetTile(x, y);
        var newTiles = new List<GameObject>();




        GameObject newTile1;
        GameObject newTile2;
        GameObject newTile3;
        GameObject newTile4;
        GameObject newTile5;
        GameObject newTile6;

        board.ExplodeTileNonRecursive(board.GetTile(x, y));
        newTile1 = board.CreateHorizontalStripedTile(x, y, CandyColor.Black);
        newTiles.Add(newTile1);

        board.ExplodeTileNonRecursive(board.GetTile(x + 1, y));
        newTile2 = board.CreateVerticalStripedTile(x + 1, y, CandyColor.Black);
        newTiles.Add(newTile2);

        board.ExplodeTileNonRecursive(board.GetTile(x - 1, y));
        newTile3 = board.CreateVerticalStripedTile(x - 1, y, CandyColor.Black);
        newTiles.Add(newTile3);

        board.ExplodeTileNonRecursive(board.GetTile(x, y));
        newTile4 = board.CreateVerticalStripedTile(x, y, CandyColor.Black);
        newTiles.Add(newTile4);

        board.ExplodeTileNonRecursive(board.GetTile(x, y + 1));
        newTile5 = board.CreateHorizontalStripedTile(x, y + 1, CandyColor.Black);
        newTiles.Add(newTile5);

        board.ExplodeTileNonRecursive(board.GetTile(x, y - 1));
        newTile6 = board.CreateHorizontalStripedTile(x, y - 1, CandyColor.Black);
        newTiles.Add(newTile6);
        /*
        if (Random.Range(0, 2) % 2 == 0)
        {
            newTile = board.CreateHorizontalStripedTile(a, b, CandyColor.Black);
        }
        else
        {
            newTile = board.CreateVerticalStripedTile(a, b, CandyColor.Black);
        }
        */

        //newTiles.Add(newTile1);


        board.ExplodeGeneratedTiles(newTiles);
    }

    private void PatlatInvoke()
    {
        Debug.Log("Sayı 1 ---- " + _sayi1);
        Debug.Log("Sayı 2 ---- " + _sayi2);

        if (_sayi1 >= 0)
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

        if (_sayi3 >= 0)
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

        GravityKontrol();
    }

    private void TekrarlaInvoke()
    {
        Invoke("PatlatInvoke", 0.1f);
    }

    private void GravityKontrol()
    {
        if (_sayi2 == cachedTiles.Count)
        {
            if (_sayi1 == -1)
            {
                _board.ApplyGravity();
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
