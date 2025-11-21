using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 lastPosition = new();
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] Grid grid;
    [SerializeField] GameObject gridVisualLvl0;
    [SerializeField] GameObject gridVisualLvl1;
    [SerializeField] GameObject gridVisualLvl2;
    [SerializeField] GameObject gridVisualLvl3;

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

        GridLevelDisplay(gridPos);
        HandleCameraMovement();
    }

    private void GridLevelDisplay(Vector3Int gridPos)
    {
        if (gridPos.y == 3 && gridVisualLvl3.activeSelf == false)
        {
            gridVisualLvl0.SetActive(false);
            gridVisualLvl1.SetActive(false);
            gridVisualLvl2.SetActive(false);
            gridVisualLvl3.SetActive(true);
        }
        else if (gridPos.y == 2 && gridVisualLvl2.activeSelf == false)
        {
            gridVisualLvl0.SetActive(false);
            gridVisualLvl1.SetActive(false);
            gridVisualLvl2.SetActive(true);
            gridVisualLvl3.SetActive(false);
        }
        else if (gridPos.y == 1 && gridVisualLvl1.activeSelf == false)
        {
            gridVisualLvl0.SetActive(false);
            gridVisualLvl1.SetActive(true);
            gridVisualLvl2.SetActive(false);
            gridVisualLvl3.SetActive(false);
        }
        else if (gridPos.y == 0 && gridVisualLvl0.activeSelf == false)
        {
            gridVisualLvl0.SetActive(true);
            gridVisualLvl1.SetActive(false);
            gridVisualLvl2.SetActive(false);
            gridVisualLvl3.SetActive(false);
        }
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        { 
            lastPosition = hit.point;
        }
        Debug.Log(lastPosition);
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
