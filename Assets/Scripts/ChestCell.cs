using UnityEngine;

[CreateAssetMenu]
public class ChestCell : CellBehaviour
{
    public override void Dig(Vector2 vector2)
    {
        base.Dig(vector2);
        FindObjectOfType<GameManager>().FoundChest();
    }
}