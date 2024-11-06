using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;       //������ ���� ����
    public Vector3 lastPostion;               //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold = 0.1f;  //�̵� ���� �Ӱ谪
    public CollectibleItem currentNearbyItem;  //���� �ֺ� ������
    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
        CheckForItems();  // Start���� ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPostion, transform.position) > moveThreshold)
        {
            CheckForItems();  // �÷��̾ �̵��ϸ� �������� �ٽ� ����
            lastPostion = transform.position;  // ������ ��ġ ����
        }

        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());  // 'E' Ű �Է� �� ������ ����
        }
    }
 

    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        CollectibleItem closestItem = null;

        foreach (Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if (item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < closestDistance)  // ���� ����� �������� ã�� ���� ���� ����
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        // ���ο� �������� �����Ǿ��� ���
        if (closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if (currentNearbyItem != null)
            {
                Debug.Log($" [E] Ű�� ���� {currentNearbyItem.itemName} ���� ");
            }
        }
    }

    // Gizmos�� ȭ�鿡 ǥ���ϴ� �޼��� (������ ���� ���� ǥ��)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
}

