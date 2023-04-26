using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameVanilla.Core;
using GameVanilla.Game.Common;

public class StripedComboRoketOlustur : MonoBehaviour
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

        Debug.Log("Debug -- 1 --");
        Debug.Log("Tile A -- " + tileA);
        Debug.Log("Tile B -- " + tileB);

        board.ComboRoketlePatlat(tileA.gameObject);
        board.ComboRoketlePatlat(tileB.gameObject);

        var x = tileB.x;
        var y = tileB.y;

        _board = board;

        _sayi1 = 0;
        _sayi2 = 0;

        _sayi3 = 0;
        _sayi4 = 0;

        tiles.Clear();
        tiles2.Clear();

        _sayi1 = x - 1;
        _sayi2 = x + 1;

        Debug.Log("Debug -- 2 --");

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

        _sayi3 = y - 1;
        _sayi4 = y + 1;

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
                    tiles2.Add(tilee);
                }
            }
            else
            {

            }


        }

        Debug.Log("Debug -- 3 --");

        cachedTiles.Clear();
        foreach (var tiled in tiles)
        {
            if (tiled != null)
            {
                cachedTiles.Add(tiled);
            }
        }

        cachedTiles2.Clear();
        foreach (var tiled in tiles2)
        {
            if (tiled != null)
            {
                cachedTiles2.Add(tiled);
            }
        }

        Invoke("PatlatInvoke", 0.1f);
        ComboRoketleriOlustur(tileB.gameObject);

        Debug.Log("Debug -- 4 --");
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
