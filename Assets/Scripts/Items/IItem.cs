using UnityEngine;

public interface IItem
{
    float Size { get; set; }
    bool CanEvolve { get; set; }
    ItemType NextItemType { get; set; }

    void Move();
}
