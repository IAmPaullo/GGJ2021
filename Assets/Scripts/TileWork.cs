using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileWork : MonoBehaviour
{
    public GridSystem gridSystem;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentCell);

        if (Input.GetKey(KeyCode.Space))
        {
            gridSystem.Dig(transform.position);
        }
    }
}
