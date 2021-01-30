using UnityEngine;

[CreateAssetMenu]
public class ChestCell : CellBehaviour
{
    public GameObject chestPrefab;
    public override void Dig(Vector2 position)
    {
        base.Dig(position);
        FindObjectOfType<GameManager>().FoundChest();
        Instantiate(chestPrefab, position, Quaternion.identity);
    }
}