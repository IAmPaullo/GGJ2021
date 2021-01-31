using UnityEngine;

[CreateAssetMenu]
public class SkeletonCell : CellBehaviour
{
    public int timeAmount;
    public GameObject skullPrefab;

    public override void Dig(Vector2 position)
    {
        base.Dig(position);
        FindObjectOfType<GameManager>().DecreaseTimer(timeAmount);
        Instantiate(skullPrefab, position, Quaternion.identity);
        FindObjectOfType<PlayerController>().TakeDamage();
    }
}
