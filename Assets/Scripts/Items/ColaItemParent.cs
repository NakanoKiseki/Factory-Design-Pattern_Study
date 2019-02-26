using UnityEngine;

public class ColaItemParent : MonoBehaviour, IItem
{
    public float Size { get; set; }
    public bool CanEvolve { get; set; }
    public ItemType NextItemType { get; set; }

    public void Move()
    {
        transform.Rotate(transform.up, 50f * Time.deltaTime);
    }
}
