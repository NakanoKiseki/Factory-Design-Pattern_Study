using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{

    private GameObject ColaParentPrefab;
    private GameObject ColaChildPrefab;
    private GameObject ColaGravePrefab;

    public ItemFactory()
    {
        ColaParentPrefab = Resources.Load<GameObject>("Prefabs/ColaParent");
        ColaChildPrefab = Resources.Load<GameObject>("Prefabs/ColaChild");
        ColaGravePrefab = Resources.Load<GameObject>("Prefabs/ColaGrave");
    }
    
    public IItem Create(ItemType itemType, Vector3 itemStartingPosition, float itemSize)
    {
        var item = GetItemByType(itemType);
        item = SetupItem(item, itemType, itemStartingPosition, itemSize);

        return item;
    }


    #region PRIVATE METHODS

    private IItem GetItemByType(ItemType itemType)
    {
        IItem item = null;

        switch (itemType)
        {
            case ItemType.Child:
                item = GameObject.Instantiate(ColaChildPrefab).AddComponent<ColaItemChild>();
                break;
            case ItemType.Parent:
                item = GameObject.Instantiate(ColaParentPrefab).AddComponent<ColaItemParent>();
                break;
            case ItemType.Grave:
                item = GameObject.Instantiate(ColaGravePrefab).AddComponent<ColaItemGrave>();
                break;
        }

        return item;
    }

    private IItem SetupItem(IItem item, ItemType itemType, Vector3 itemStartingPosition, float size)
    {
        switch (itemType)
        {
            case ItemType.Child:
                item.NextItemType = ItemType.Parent;
                break;
            case ItemType.Parent:
                item.NextItemType = ItemType.Grave;
                break;
            case ItemType.Grave:
                item.NextItemType = ItemType.Child;
                break;
        }

        ((Component)item).transform.position = itemStartingPosition;
        item.Size = size;
        ((Component)item).transform.localScale = new Vector3(size, size, size);


        return item;
    }

    #endregion
}
