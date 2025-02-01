using UnityEngine;

public class Controller_sc : MonoBehaviour
{
    private Transform pickup;
    int layerMask;
    Vector3 mousePos;
    private readonly float gridSize = 2f;

    private void Start()
    {
        layerMask = 1 << 3;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                pickup = hit.collider.transform.root;
            }
        }

        Ray floorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(floorRay, out RaycastHit floorHit, Mathf.Infinity, layerMask))
        {
            mousePos = floorHit.point;
            if (pickup != null)
            {
                mousePos = SnapVectorToGrid(mousePos);
                pickup.position = mousePos;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            pickup = null;
        }
    }

    private Vector3 SnapVectorToGrid(Vector3 vector)
    {
        float x = Mathf.Round(vector.x / gridSize) * gridSize;
        float y = Mathf.Round(vector.y / gridSize) * gridSize;
        float z = Mathf.Round(vector.z / gridSize) * gridSize;
        return new Vector3(x, y, z);
    }
}
