using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    [SerializeField] GameObject _cellPrefab;
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] Transform _gridOrigin;
    [SerializeField] Tilemap _tileMap;
    [SerializeField] Tilemap _chestTilemap;
    [SerializeField] TileBase _holeTile;
    [SerializeField] List<CellConfig> _cellConfig;

    private List<Cell> grid;
    private Vector3Int _currentCell;

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

                if (config != null)
                    tile = config.tile;

                Cell cell = new Cell(cellPosition, tile);
                grid.Add(cell);
            }
        }
    }

    public void Dig(Vector2 cellPosition)
    {
        _currentCell = _tileMap.WorldToCell(cellPosition);
        Cell cell = grid.Find(c => c.position.x == _currentCell.x && c.position.y == _currentCell.y);

        if(!cell.isDigged)
        {
            if (cell.tile != null)
                _tileMap.SetTile(_currentCell, cell.tile);
            else
                _tileMap.SetTile(_currentCell, _holeTile);

            cell.isDigged = true;

        }
            Debug.Log($"({cell.position.x}, {cell.position.y})");
    }
}

[System.Serializable]
public class CellConfig
{
    public int x;
    public int y;
    public TileBase tile;
}

public class Cell
{
    public Vector3Int position;
    public bool isDigged;
    public TileBase tile;

    public Cell(Vector3Int position, TileBase tile)
    {
        this.position = position;
        this.isDigged = false;
        this.tile = tile;
    }
}
