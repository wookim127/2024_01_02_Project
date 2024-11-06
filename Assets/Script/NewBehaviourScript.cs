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
    public float checkRadius = 3.0f;       //아이템 감지 범위
    public Vector3 lastPostion;               //플레이어의 마지막 위치 (플레이어 이동이 감지 될 경우 주변을 찾기 위한 변수)
    public float moveThreshold = 0.1f;  //이동 감지 임계값
    public CollectibleItem currentNearbyItem;  //현재 주변 아이템
    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
        CheckForItems();  // Start에서 아이템 감지
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPostion, transform.position) > moveThreshold)
        {
            CheckForItems();  // 플레이어가 이동하면 아이템을 다시 감지
            lastPostion = transform.position;  // 마지막 위치 갱신
        }

        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());  // 'E' 키 입력 시 아이템 수집
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
                if (distance < closestDistance)  // 가장 가까운 아이템을 찾기 위한 조건 수정
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        // 새로운 아이템이 감지되었을 경우
        if (closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if (currentNearbyItem != null)
            {
                Debug.Log($" [E] 키를 눌러 {currentNearbyItem.itemName} 수집 ");
            }
        }
    }

    // Gizmos를 화면에 표시하는 메서드 (아이템 감지 범위 표시)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
}

