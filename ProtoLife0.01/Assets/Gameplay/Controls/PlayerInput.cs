using TMPro;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 lastPosition = new();
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] Grid grid;
    [SerializeField] GameObject gridVisual;
    [SerializeField] private GameData data;
    [SerializeField] TextMeshProUGUI cellData;
    [SerializeField] GameObject TestLichen;

    [SerializeField] GameObject CameraRig;
    float cameraMovementSpeed = 0.1f;
    float cameraMovementTime = 5;
    Vector3 newPosition;

    private void Start()
    {
        newPosition = CameraRig.transform.position;
    }

    void Update()
    {
        Vector3 mousePos = GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        UICellData(gridPos);
        GridLevelDisplay(grid.CellToWorld(gridPos));
        HandleCameraMovement();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            PlaceItem(gridPos);
        }
    }

    private void PlaceItem(Vector3Int gridPos)
    {
        //TODO: If placement is possible

        Instantiate(TestLichen);
        TestLichen.transform.position = gridPos;
        TestLichen.transform.eulerAngles = new Vector3(
            TestLichen.transform.eulerAngles.x,
            TestLichen.transform.eulerAngles.y + Random.Range(0, 360),
            TestLichen.transform.eulerAngles.z);
    }

    private void UICellData(Vector3 mousePos)
    {
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        Debug.Log(gridPos);
        if (data.cellDatabase.ContainsKey(gridPos))
        {
            Cell selectedCell = data.cellDatabase[gridPos];
            cellData.text =
                $"Coordinates: {gridPos} " +
                $"\nHeight: {selectedCell.height}  " +
                $"\nHumidity: {selectedCell.humidity} " +
                $"\nNutrients: {selectedCell.nutrients} " +
                $"\nLifeform: {(selectedCell.lifeform != null ? selectedCell.lifeform : "None")} " +
                $"\nMycelium: {(selectedCell.mycelium != null ? selectedCell.mycelium : "None")}";
        }
    }

    private void GridLevelDisplay(Vector3 gridPos)
    {
        gridVisual.transform.position = new Vector3(gridPos.x, gridPos.y + 0.1f, gridPos.z);
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 400, placementLayerMask))
        { 
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * cameraMovementSpeed);
            newPosition += (transform.right * cameraMovementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -cameraMovementSpeed);
            newPosition += (transform.right * -cameraMovementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * cameraMovementSpeed);
            newPosition += (transform.forward * -cameraMovementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -cameraMovementSpeed);
            newPosition += (transform.forward * cameraMovementSpeed);
        }

        CameraRig.transform.position = Vector3.Lerp(CameraRig.transform.position, newPosition, Time.deltaTime * cameraMovementTime);
    }
}
