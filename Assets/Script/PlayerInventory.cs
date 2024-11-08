using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;


    public void AddItem(ItemType itemType)
    {
        //아이템 종류에 따른 다른 동작 수행
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {crystalCount}");    //현재 크리스탈 개수 출력
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"식물 획득 ! 현재 개수 : {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {bushCount}");    //현재 크리스탈 개수 출력
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {treeCount}");    //현재 크리스탈 개수 출력
                break;
        }

    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
        
    }

    private void ShowInventory()
    {
        Debug.Log("=======인벤토리=======");
        Debug.Log($"크리스탈:{crystalCount}개");
        Debug.Log($"식물:{crystalCount}개");
        Debug.Log($"수풀:{crystalCount}개");
        Debug.Log($"나무:{crystalCount}개");
        Debug.Log("======================");
    }
}
