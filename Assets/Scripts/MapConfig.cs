using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public List<CellConfig> cellConfig;
}

[System.Serializable]
public class CellConfig
{
    public int x;
    public int y;
    public CellBehaviour cellBehaviour;
}