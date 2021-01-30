using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cell
{
    public Vector3Int position;
    public bool isDigged;
    public TileBase tile;
    public TileType tileType;
    public Action OnDig;

    public Cell(Vector3Int position, TileBase tile, TileType tileType, Action onDig)
    {
        this.position = position;
        isDigged = false;
        this.tile = tile;
        this.tileType = tileType;
    }
    
    public virtual void Dig() 
    {
        OnDig?.Invoke();
    }
}