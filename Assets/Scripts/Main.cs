using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public int StartItemNum = 10;
    public Vector3 NewItemPosition;
    public float NewItemPositionVariationRange = 5f;
    public float NewItemSize = 1.0f;
    public float NewItemSizeVariationRange = 0.3f;

    private List<IItem> itemList = new List<IItem>();
    private ItemFactory itemFactory;
    
    #region ENTRY POINT

    private void Awake()
    {
        itemFactory = new ItemFactory();
        var itemTypeLength = Enum.GetNames(typeof(ItemType)).Length;
        for (var i = 0; i < StartItemNum; i++)
            CreateItem((ItemType)(i % itemTypeLength));
    }

    #endregion


    #region MAIN LOOP

    private void Update()
    {

        for (var i = itemList.Count - 1; i >= 0; i--)
        {
            var item = itemList[i];
            if (item.CanEvolve == true)
            {
                CreateItem(item.NextItemType, ((Component)item).transform.position);
                var itemObj = ((Component)item).gameObject;

                itemList.RemoveAt(i);

                Destroy(itemObj);
            }

            item.Move();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var evolveItemNum = Random.Range(0, itemList.Count);
            itemList[evolveItemNum].CanEvolve = true;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            var evolveItemNum = Random.Range(0, itemList.Count);
            itemList[evolveItemNum].CanEvolve = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            foreach (var item in itemList)
            {
                item.CanEvolve = true;
            }
        }
    }

    #endregion

    #region PRIVATE METHODS

    private void CreateItem(ItemType itemType)
    {
        var item = itemFactory.Create(itemType, GetRandomStartingPosition(), NewItemSize);
        itemList.Add(item);
    }

    private void CreateItem(ItemType itemType, Vector3 position)
    {
        var item = itemFactory.Create(itemType, position, NewItemSize);
        itemList.Add(item);
    }

    private Vector3 GetRandomStartingPosition()
    {
        var itemX = Random.Range(NewItemPosition.x - NewItemPositionVariationRange,
                                 NewItemPosition.x + NewItemPositionVariationRange);
        var itemY = NewItemPosition.y;
        var itemZ = Random.Range(NewItemPosition.z - NewItemPositionVariationRange,
                                 NewItemPosition.z + NewItemPositionVariationRange);

        return new Vector3(itemX, itemY, itemZ);
    }

    private float GetRandomItemSize()
    {
        return NewItemSize + Random.Range(-NewItemSizeVariationRange, NewItemSizeVariationRange);
    }

    #endregion
}
