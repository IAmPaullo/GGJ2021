using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cell
{
    public Vector2 worldPosition;
    public Vector3Int tilemapPosition;
    public bool isDigged;
    public TileBase tile;
    public TileType tileType;
    public Action<Vector2> OnDig;

    public Cell(Vector2 worldPosition, Vector3Int position, TileBase tile, TileType tileType, Action<Vector2> onDigCallback)
    {
        this.worldPosition = worldPosition;
        this.tilemapPosition = position;
        isDigged = false;
        this.tile = tile;
        this.tileType = tileType;
        OnDig = onDigCallback;
    }
    
    public virtual void Dig(Vector2 position) 
    {
        OnDig?.Invoke(position);
    }
}