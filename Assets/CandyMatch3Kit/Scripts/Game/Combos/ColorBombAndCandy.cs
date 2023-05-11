using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Common;
using UnityEngine;

public class ColorBombAndCandy : MonoBehaviour
{
    private int _sayi1;

    private int _tileSayisi;

    GameBoard _board;

    private Tile _candy;

    public void Resolve(GameBoard board, Tile tileA, Tile tileB)
    {
        _board = board;

        var bomb = tileA.GetComponent<ColorBomb>() != null ? tileA : tileB;
        board.ExplodeTileNonRecursive(bomb.gameObject);

        var explosion = board.fxPool.colorBombExplosion.GetObject();
        explosion.transform.position = tileB.transform.position;

        var candy = tileA.GetComponent<Candy>() != null ? tileA : tileB;
        _candy = candy;


        for (var i = board.tiles.Count - 1; i >= 0; i--)
        {
            var tile = board.tiles[i];
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
            {
                board.ExplodeTileNonRecursive(tile);
            }

        }


        //_sayi1 = 0;
        //_tileSayisi = board.tiles.Count;

        //Invoke("PatlatInvoke", 0.01f);

        SoundManager.instance.PlaySound("ColorBomb");

        board.ApplyGravity();
    }

    private void PatlatInvoke()
    {
        var tile = _board.tiles[_sayi1];
        if (tile != null && tile.GetComponent<Candy>() != null &&
            tile.GetComponent<Candy>().color == _candy.GetComponent<Candy>().color)
        {
            _board.ExplodeTileNonRecursive(tile);
        }

        _sayi1++;

        GravityKontrol();
    }

    private void TekrarlaInvoke()
    {
        Invoke("PatlatInvoke", 0.01f);
    }

    private void GravityKontrol()
    {
        if (_sayi1 == _tileSayisi)
        {
            _board.ApplyGravity();
        }
        else
        {
            TekrarlaInvoke();
        }
    }
}
