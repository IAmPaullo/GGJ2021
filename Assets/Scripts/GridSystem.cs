﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool _debug;
    [SerializeField] GameObject _normalCellMarker;
    [SerializeField] GameObject _skullCellMarker;
    [SerializeField] GameObject _chestCellMarker;

    [Header("RandomSettings")]
    [SerializeField] SkeletonCell _skeletonCell;
    [SerializeField] ChestCell _chestCell;

    [Header("Grid Settings")]
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] Transform _gridOrigin;
    [SerializeField] float _digDelay = 0.5f; 
    [SerializeField] List<MapConfig> _mapConfigs;

    [Header("TileMaps Settings")]
    [SerializeField] Tilemap _tileMapHoles;
    [SerializeField] Tilemap _tileMap;
    [SerializeField] TileBase _holeTile;
    [SerializeField] TumbaCell _tumbaCellBehaviour;

    private List<Cell> grid;
    private Vector3Int _currentCellPosition;

    private void Start()
    {
        grid = new List<Cell>(_width * _height);
        MapConfig mapConfig = _mapConfigs[UnityEngine.Random.Range(0, _mapConfigs.Count)];
        Debug.Log($"[GridSystem] Layout {mapConfig.name} loaded!");

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 position = new Vector2(_gridOrigin.position.x + x, _gridOrigin.position.y + y);
                Vector3Int cellPosition = _tileMap.WorldToCell(position);

                CellConfig config = mapConfig.cellConfig.Find(c => c.x == cellPosition.x && c.y == cellPosition.y);
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

                if(_debug)
                {
                    switch (tileType)
                    {
                        case TileType.NORMAL:
                            Instantiate(_normalCellMarker, position, Quaternion.identity);
                            break;
                        case TileType.SKELETON:
                            Instantiate(_skullCellMarker, position, Quaternion.identity);
                            break;
                        case TileType.CHEST:
                            Instantiate(_chestCellMarker, position, Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }
    public bool CanDig(Vector2 playerPosition)
    {
        var cellPos = _tileMap.WorldToCell(playerPosition);
        Cell cell = GetCellAtPosition(cellPos);

        float roundedX = RoundToNearestHalf(playerPosition.x); //(Mathf.Round(playerPosition.x * 100)) / 100.0f;
        float roundedY = RoundToNearestHalf(playerPosition.y);//(Mathf.Round(playerPosition.y * 100)) / 100.0f;
        Vector2 playerPos = new Vector2(roundedX, roundedY);
        
        return playerPos.x == cell.worldPosition.x && playerPos.y == cell.worldPosition.y;
    }

    public float RoundToNearestHalf(float a)
    {
        return a = Mathf.Round(a * 2f) * 0.5f;
    }
    public TileType Dig(Vector2 playerPosition)
    {
        _currentCellPosition = _tileMap.WorldToCell(playerPosition);
        Cell cell = GetCellAtPosition(_currentCellPosition);

        int adjacents = GetAdjacentSkeletons();
        Debug.Log($"Adjacents: {adjacents}");

        if (cell != null)
        {
            if (!cell.isDigged)
            {
                if (cell.tile != null)
                {
                    ChangeTile(_tileMap, cell.tile, _digDelay, () =>
                    {
                        cell.Dig(cell.worldPosition);
                    });
                }
                else if (adjacents > 0)
                {
                    ChangeTile(_tileMapHoles, _holeTile, _digDelay, () =>
                    {
                        _tumbaCellBehaviour.Dig(cell.worldPosition);
                    });
                }
                else
                {
                    ChangeTile(_tileMapHoles, _holeTile, _digDelay, null);
                }

                cell.isDigged = true;
            }
            
            Debug.Log($"({cell.tilemapPosition.x}, {cell.tilemapPosition.y})");

            return cell.tileType;
        }

        return TileType.NORMAL;
    }

    private void ChangeTile(Tilemap tilemap, TileBase tile, float delay, Action onChanged)
    {
        StartCoroutine(DoAfterTime(delay, () =>
        {
            tilemap.SetTile(_currentCellPosition, tile);
            onChanged?.Invoke();
        }));
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
                if(!cell.isDigged)
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

    private IEnumerator DoAfterTime(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
}
