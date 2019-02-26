using UnityEngine;

public class ColaItemGrave : MonoBehaviour, IItem
{
    public float Size { get; set; }
    public bool CanEvolve { get; set; }
    public ItemType NextItemType { get; set; }

    public void Move()
    {
        // Can't move.
    }
}
