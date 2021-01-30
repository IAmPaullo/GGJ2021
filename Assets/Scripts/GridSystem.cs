using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    [SerializeField] GameObject _cellPrefab;
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] Transform _gridOrigin;
    [SerializeField] Tilemap _tileMap;
    [SerializeField] TileBase _holeTile;
    [SerializeField] List<CellConfig> _cellConfig;

    private List<Cell> grid;
    private Vector3Int _currentCellPosition;

    private void Start()
    {
        grid = new List<Cell>(_width * _height);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 position = new Vector2(_gridOrigin.position.x + x, _gridOrigin.position.y + y);
                Vector3Int cellPosition = _tileMap.WorldToCell(position);

                CellConfig config = _cellConfig.Find(c => c.x == cellPosition.x && c.y == cellPosition.y);
                TileBase tile = null;
                TileType tileType = TileType.NORMAL;

                if (config != null)
                {
                    tile = config.cellBehaviour.tile;
                    tileType = config.cellBehaviour.tileType;
                }

                Cell cell = new Cell(position, cellPosition, tile, tileType, 
                (cellPos) => 
                {
                    config.cellBehaviour.Dig(cellPos);
                });

                grid.Add(cell);
            }
        }
    }

    public void Dig(Vector2 cellPosition)
    {
        _currentCellPosition = _tileMap.WorldToCell(cellPosition);
        Cell cell = GetCellAtPosition(_currentCellPosition);

        if(!cell.isDigged)
        {
            if (cell.tile != null)
            {
                _tileMap.SetTile(_currentCellPosition, cell.tile);
                cell.Dig(cell.worldPosition);
            }
            else
            {
                _tileMap.SetTile(_currentCellPosition, _holeTile);
            }

            cell.isDigged = true;

            int adjacents = GetAdjacentSkeletons();
            Debug.Log($"Adjacents: {adjacents}");
        }
            
        Debug.Log($"({cell.tilemapPosition.x}, {cell.tilemapPosition.y})");
    }

    public int GetAdjacentSkeletons()
    {
        int total = 0;

        List<Vector3Int> adjacentsCoords = new List<Vector3Int>()
        {
            new Vector3Int(_currentCellPosition.x - 1, _currentCellPosition.y, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x + 1, _currentCellPosition.y, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x, _currentCellPosition.y - 1, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x, _currentCellPosition.y + 1, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x - 1, _currentCellPosition.y - 1, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x + 1, _currentCellPosition.y + 1, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x - 1, _currentCellPosition.y + 1, _currentCellPosition.z),
            new Vector3Int(_currentCellPosition.x + 1, _currentCellPosition.y - 1, _currentCellPosition.z),
        };

        foreach (var coord in adjacentsCoords)
        {
            Cell cell = GetCellAtPosition(coord);

            if (cell != null)
            {
                if (cell.tileType == TileType.SKELETON)
                    total++;
            }
        }

        return total;
    }

    public Cell GetCellAtPosition(Vector3Int position)
    {
        return grid.Find(c => c.tilemapPosition.x == position.x && c.tilemapPosition.y == position.y);
    }
}

[Serializable]
public class CellConfig
{
    public int x;
    public int y;
    public CellBehaviour cellBehaviour;
}