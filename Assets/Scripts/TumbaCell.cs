using UnityEngine;

[CreateAssetMenu]
public class TumbaCell : CellBehaviour
{
    public GameObject tumbaPrefab;

    public override void Dig(Vector2 position)
    {
        base.Dig(position);
        var tumba = Instantiate(tumbaPrefab, position, Quaternion.identity);
        tumba.GetComponent<Tumba>().SetText("3");
    }
}