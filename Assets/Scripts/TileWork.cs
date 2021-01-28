using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileWork : MonoBehaviour
{
    public Tile holeTile;
    public Tilemap tileMap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int currentCell = tileMap.WorldToCell(transform.position);
        //Debug.Log(currentCell);

        if (Input.GetKey(KeyCode.Space))
        {
            tileMap.SetTile(currentCell, null);
            tileMap.SetTile(currentCell, holeTile);

        }
    }
}
