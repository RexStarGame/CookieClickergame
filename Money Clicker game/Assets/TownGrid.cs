using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownGrid : MonoBehaviour
{
    #region Tilemap Management

    public static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    public static void SetTileBlock(BoundsInt area, TileBase[] tileBase, Tilemap tilemap)
    {
        tilemap.SetTilesBlock(area, tileBase);
    }

    public static void FillTiles(TileBase[] arr, TileBase tileBase)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBase;
        }
    }

    public void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        SetTileBlock(area, null, tilemap);
    }
    #endregion
}