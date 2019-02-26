using UnityEngine;

public class ColaItemChild : MonoBehaviour, IItem
{
    public float Size { get; set; }
    public bool CanEvolve { get; set; }
    public ItemType NextItemType { get; set; }

    public void Move()
    {
        transform.position += Vector3.up * 3f * Time.deltaTime;
    }
}
