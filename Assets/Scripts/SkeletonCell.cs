using UnityEngine;

[CreateAssetMenu]
public class SkeletonCell : CellBehaviour
{
    public int timeAmount;

    public override void Dig()
    {
        base.Dig();
        FindObjectOfType<GameManager>().DecreaseTimer(timeAmount);
    }
}