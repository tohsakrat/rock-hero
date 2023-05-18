using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //static Shop instance;

    public GameObject ItemGrid;
    public Item item;


    // Start is called before the first frame update
    void Start()
    {
        //int num = Hero.r.items.Count;
        //Debug.Log("物品栏数量:");
        //Debug.Log(num);


        //foreach (Pickup i in Hero.r.items)
        //{
        //    Debug.Log("物品名称：");
        //    Debug.Log(i.name);
        //    CreateNewItem(item, ItemGrid, i.sp);
        //}

        //foreach (skills s in hero.r.skills)
        //{
        //    debug.log("技能名称");
        //    debug.log(i.name);
        //}

        //Debug.Log(Hero.r.Skills);
    }

    public void OnEnable()
    {
        int num = Hero.r.items.Count;
        Debug.Log("物品栏数量:");
        Debug.Log(num);

        foreach (Pickup i in Hero.r.items)
        {
            Debug.Log("物品名称：");
            Debug.Log(i.name);
            CreateNewItem(item, ItemGrid, i.sp);
        }
    }

    public void OnDisable()
    {
        ClearItem(ItemGrid);
    }


    public static void CreateNewItem(Item item, GameObject ItemGrid, Sprite itemSprite)
    {
        Item newItem = Instantiate(item, ItemGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(ItemGrid.transform);
        
        newItem.item = item;
        newItem.itemImage.sprite = itemSprite;
    }

    private void ClearItem(GameObject ItemGrid)
    {
        if(ItemGrid.transform.childCount > 0)
        {
            for (int i =0; i < ItemGrid.transform.childCount; i++)
            {
                Destroy(ItemGrid.transform.GetChild(i).gameObject);
            }
        }
    }
}
