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
        //������ ������ ���� �ٸ� ���� ����
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� : {crystalCount}");    //���� ũ����Ż ���� ���
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"�Ĺ� ȹ�� ! ���� ���� : {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� : {bushCount}");    //���� ũ����Ż ���� ���
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� : {treeCount}");    //���� ũ����Ż ���� ���
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
        Debug.Log("=======�κ��丮=======");
        Debug.Log($"ũ����Ż:{crystalCount}��");
        Debug.Log($"�Ĺ�:{crystalCount}��");
        Debug.Log($"��Ǯ:{crystalCount}��");
        Debug.Log($"����:{crystalCount}��");
        Debug.Log("======================");
    }
}
