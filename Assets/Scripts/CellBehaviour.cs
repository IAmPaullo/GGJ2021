using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CellBehaviour : ScriptableObject
{
    public TileBase tile;
    public TileType tileType;
    public UnityEvent OnDig;

    public virtual void Dig()
    {
        OnDig?.Invoke();
    }
}