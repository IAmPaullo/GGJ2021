using UnityEngine;

[CreateAssetMenu]
public class TumbaCell : CellBehaviour
{
    public GameObject skullPrefab;

    public override void Dig(Vector2 position)
    {
        base.Dig(position);
        Instantiate(skullPrefab, position, Quaternion.identity);
    }
}