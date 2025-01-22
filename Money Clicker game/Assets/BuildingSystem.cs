using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq; // Tilføjet for LINQ funktionalitet

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public TileBase[] takenTile;

    #region Building Placement

    private void Awake()
    {
        current = this; // Initialiser singleton
    }

    public void InitializeWithObject(GameObject building, Vector3 pos)
    {
        pos.z = 0;
        pos.y -= building.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        Vector3 position = gridLayout.CellToLocalInterpolated(cellPos);

        GameObject obj = Instantiate(building, position, Quaternion.identity);
        PlaceableObject temp = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = TownGrid.GetTileBlock(area, MainTilemap);
        return !baseArray.Any(tile => takenTile.Contains(tile));
    }

    public void TakeArea(BoundsInt area)
    {
        TileBase[] tiles = new TileBase[area.size.x * area.size.y];
        TownGrid.FillTiles(tiles, takenTile[0]); // Brug første tile i takenTile array
        TownGrid.SetTileBlock(area, tiles, MainTilemap);
    }
    #endregion
}