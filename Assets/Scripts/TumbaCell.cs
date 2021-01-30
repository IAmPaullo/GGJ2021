using UnityEngine;

[CreateAssetMenu]
public class TumbaCell : CellBehaviour
{
    public GameObject tumbaPrefab;

    public override void Dig(Vector2 position)
    {
        
        base.Dig(position);
        var tumba = Instantiate(tumbaPrefab, position, Quaternion.identity);
        int adj = FindObjectOfType<GridSystem>().GetAdjacentSkeletons();
        tumba.GetComponent<Tumba>().SetText(adj.ToString());
        
    }
}
