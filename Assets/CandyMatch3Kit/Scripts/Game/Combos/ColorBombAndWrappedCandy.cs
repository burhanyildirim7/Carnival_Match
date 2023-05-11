using System.Collections;
using System.Collections.Generic;
using GameVanilla.Core;
using GameVanilla.Game.Common;
using UnityEngine;

public class ColorBombAndWrappedCandy : MonoBehaviour
{
    public void Resolve(GameBoard board, Tile tileA, Tile tileB)
    {
        var bomb = tileA.GetComponent<ColorBomb>() != null ? tileA : tileB;
        board.ExplodeTileNonRecursive(bomb.gameObject);

        var explosion = board.fxPool.colorBombExplosion.GetObject();
        explosion.transform.position = tileB.transform.position;

        var wrapped = tileA.GetComponent<WrappedCandy>() != null ? tileA : tileB;

        var newTiles = new List<GameObject>();
        for (var i = board.tiles.Count - 1; i >= 0; i--)
        {
            var tile = board.tiles[i];
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == CandyColor.Blue)
            {
                var x = tile.GetComponent<Tile>().x;
                var y = tile.GetComponent<Tile>().y;
                board.ExplodeTileNonRecursive(tile);
                var newTile = board.CreateWrappedTile(x, y, wrapped.GetComponent<Candy>().color);
                newTiles.Add(newTile);
            }
        }

        SoundManager.instance.PlaySound("ColorBomb");

        board.ExplodeGeneratedTiles(newTiles);
    }
}
