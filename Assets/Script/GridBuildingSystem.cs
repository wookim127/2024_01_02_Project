using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector3Int Position;
    public bool IsOccupied;
    public GameObject Building;
    public GridCell(Vector3Int position)
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}
public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] float cellSize = 1.0f;
    [SerializeField] private GameObject cellPrefabs;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Grid grid;
    private GridCell[,] cells;
    private Camera firstPersonCamera;

    void Start()
    {
        firstPersonCamera = playerController.firstPersonCamera;
        CreateGrid();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid. GetCellCenterWorld(new Vector3Int(x, 0, z));
                Gizmos.DrawWireCube(cellCenter,new Vector3(cellSize, 0.1f, cellSize));
            }
        }
    }

    

    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);

        cells = new GridCell[width, height];
    Vector3 gridCenter = playerController.transform.position;
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2.0f);

        for(int x = 0; x < width;x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);
                GameObject cellObject = Instantiate(cellPrefabs, worldPosition, cellPrefabs.transform.rotation);
                cellObject.transform.SetParent(transform);
            }
        }
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
