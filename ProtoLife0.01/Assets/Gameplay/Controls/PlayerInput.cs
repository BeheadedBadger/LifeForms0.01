using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 lastPosition = new();
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] Grid grid;

    void Update()
    {
        Vector3 mousePos = GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
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
}
