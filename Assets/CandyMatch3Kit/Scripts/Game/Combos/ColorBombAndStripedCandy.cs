using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Common;
using UnityEngine;

public class ColorBombAndStripedCandy : MonoBehaviour
{
    private GameBoard _board;

    private Tile striped;

    private int _sayi1;

    private int _sayi2;

    private List<GameObject> newTiles = new List<GameObject>();

    public void Resolve(GameBoard board, Tile tileA, Tile tileB)
    {
        _board = board;

        _sayi1 = 0;
        _sayi2 = 0;

        var bomb = tileA.GetComponent<ColorBomb>() != null ? tileA : tileB;
        board.ExplodeTileNonRecursive(bomb.gameObject);

        var explosion = board.fxPool.colorBombExplosion.GetObject();
        explosion.transform.position = tileB.transform.position;

        striped = tileA.GetComponent<StripedCandy>() != null ? tileA : tileB;

        _sayi1 = board.tiles.Count - 1;

        board._colorBombAktif = true;
        SoundManager.instance.PlaySound("ColorBomb");

        StartCoroutine(Olustur());
    }

    private IEnumerator Olustur()
    {
        for (var i = _board.tiles.Count - 1; i >= 0; i--)
        {
            var tile = _board.tiles[i];
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == CandyColor.Blue)
            {
                var x = tile.GetComponent<Tile>().x;
                var y = tile.GetComponent<Tile>().y;
                _board.ExplodeTileNonRecursive(tile);
                GameObject newTile;
                if (Random.Range(0, 2) % 2 == 0)
                {
                    newTile = _board.CreateHorizontalStripedTile(x, y, striped.GetComponent<Candy>().color);
                }
                else
                {
                    newTile = _board.CreateVerticalStripedTile(x, y, striped.GetComponent<Candy>().color);
                }

                newTiles.Add(newTile);

                yield return new WaitForSeconds(0.1f);
            }
        }

        StartCoroutine(Patlat());


    }

    private IEnumerator Patlat()
    {

        for (int i = 0; i < newTiles.Count; i++)
        {

            if (newTiles[i].GetComponent<ColorBomb>() != null)
            {
                _board.ColorBombPatlat(newTiles[i]);
            }
            else if (newTiles[i].GetComponent<StripedCandy>() != null && newTiles[i] != gameObject)
            {
                if (newTiles[i].GetComponent<StripedCandy>()._patladim == true)
                {

                }
                else
                {
                    _board.RoketlePatlat(newTiles[i]);
                }

            }
            else
            {
                _board.BoosterIlePatlat(newTiles[i]);
            }





            yield return new WaitForSeconds(0.1f);

        }

        yield return new WaitForSeconds(0.1f);

        _board.ApplyGravity();

        _board._colorBombAktif = false;


    }

    private void OlusturInvoke()
    {
        var tile = _board.tiles[_sayi1];
        if (tile != null && tile.GetComponent<Candy>() != null &&
            tile.GetComponent<Candy>().color == CandyColor.Blue)
        {
            var x = tile.GetComponent<Tile>().x;
            var y = tile.GetComponent<Tile>().y;
            _board.ExplodeTileNonRecursive(tile);
            GameObject newTile;

            if (Random.Range(0, 2) == 1)
            {
                newTile = _board.CreateHorizontalStripedTile(x, y, striped.GetComponent<Candy>().color);
            }
            else
            {
                newTile = _board.CreateVerticalStripedTile(x, y, striped.GetComponent<Candy>().color);
            }

            //newTile = _board.CreateHorizontalStripedTile(x, y, striped.GetComponent<Candy>().color);

            newTiles.Add(newTile);
        }

        _sayi1--;

        TekrarlaOlusturInvoke();
    }

    private void PatlatInvoke()
    {
        if (newTiles[_sayi2] != null)
        {
            if (newTiles[_sayi2].GetComponent<ColorBomb>() != null)
            {
                _board.ColorBombPatlat(newTiles[_sayi2]);
            }
            else if (newTiles[_sayi2].GetComponent<StripedCandy>() != null && newTiles[_sayi2] != gameObject)
            {
                if (newTiles[_sayi2].GetComponent<StripedCandy>()._patladim == true)
                {

                }
                else
                {
                    _board.RoketlePatlat(newTiles[_sayi2]);
                }

            }
            else
            {
                _board.BoosterIlePatlat(newTiles[_sayi2]);
            }

            _sayi2++;

        }
        else
        {

        }

        GravityKontrol();
    }

    private void TekrarlaOlusturInvoke()
    {
        if (_sayi1 >= 0)
        {
            //Invoke("OlusturInvoke", 0.05f);
        }
        else
        {
            PatlatInvoke();
        }

    }

    private void TekrarlaInvoke()
    {
        Invoke("PatlatInvoke", 0.1f);
    }

    private void GravityKontrol()
    {
        if (_sayi2 == newTiles.Count)
        {

            _board.ApplyGravity();


        }
        else
        {
            //TekrarlaInvoke();
        }
    }
}
