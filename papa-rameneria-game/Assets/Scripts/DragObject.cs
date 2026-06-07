using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjects : MonoBehaviour
{
    [Header("Snapping")]
    // public Transform dropZone; 
    public string dropZoneTag = "DropZone";
    public float snapDistance = 0.25f;

    private Vector3 originalPosition;
    private bool isDragging = false;
    private Vector3 mouseOffset;
    // private float lockedZPosition;
    private Quaternion originalRotation;

    void Start()
    {
         // Save the start position in case the drop fails
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        // lockedZPosition = transform.position.z;
    }

    void Update()
    {
        if (transform.position.y <= 0.5f)
        {
            transform.position = originalPosition;
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;
        
        // Calculate the difference between object position and mouse world position
        transform.SetParent(null);
        mouseOffset = transform.position - GetMouseWorldPos();
        isDragging = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            // rb.linearVelocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;
        }
        
    }

    void OnMouseDrag()
    {
        Vector3 curPosition = GetMouseWorldPos() + mouseOffset;
        // curPosition.z = lockedZPosition; 
        if (isDragging)
        {
            // Move the object to the mouse position, keeping the z-offset
            transform.position = curPosition;
            transform.rotation = originalRotation;   //keep rotation the same
        }
    }


    void OnMouseUp()
    {
        isDragging = false;
        Rigidbody rb = GetComponent<Rigidbody>();
            
        GameObject[] dropZones = GameObject.FindGameObjectsWithTag(dropZoneTag);

        Transform closestZone = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject zone in dropZones)
        {
            float distance = Vector3.Distance(transform.position, zone.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestZone = zone.transform;
            }
        }
                
        if (closestZone != null && closestDistance <= snapDistance)
        {
            transform.position = closestZone.position;
            transform.rotation = originalRotation;

            transform.SetParent(closestZone, true);

        }
        else
        {
            transform.SetParent(null, true);

        }
        
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    private Vector3 GetMouseWorldPos()
    {
        // Convert mouse screen position to world coordinates
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
