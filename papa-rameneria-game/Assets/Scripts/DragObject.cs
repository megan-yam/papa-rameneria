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
    private Quaternion originalRotation;

    void Start()
    {
         // Save the start position in case the drop fails
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        Debug.Log(originalPosition);
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
        StartDragging();
        
    }

    void OnMouseDrag()
    {
        Vector3 curPosition = GetMouseWorldPos() + mouseOffset;
        // curPosition.z = lockedZPosition; 
        if (isDragging)
        {
            Debug.Log(transform.position);
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

        bool zoneIsEmpty = false;

        if (closestZone != null)
        {
            zoneIsEmpty = closestZone.childCount == 0;
        }
                
        // if (closestZone != null && closestDistance <= snapDistance)
        // {
        //     transform.position = closestZone.position;
        //     transform.rotation = originalRotation;

        //     transform.SetParent(closestZone, true);

        //     if (rb != null)
        //     {
        //         rb.isKinematic = true;
        //         rb.useGravity = false;
        //     }
        // }
        if (closestZone != null && closestDistance <= snapDistance && zoneIsEmpty)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            if (renderers.Length > 0)
            {
                Bounds bounds = renderers[0].bounds;

                for (int i = 1; i < renderers.Length; i++)
                {
                    bounds.Encapsulate(renderers[i].bounds);
                }

                Vector3 centerOffset = bounds.center - transform.position;
                transform.position = closestZone.position - centerOffset;
            }
            else
            {
                transform.position = closestZone.position;
            }

            transform.rotation = originalRotation;
            transform.SetParent(closestZone, true);
            Bowl bowl = closestZone.GetComponentInParent<Bowl>();

            Ingredient ingredient = GetComponent<Ingredient>();

            if (bowl != null && ingredient != null)
            {
                bowl.AddIngredient(ingredient.ingredientType);
            }

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
        else
        {
             transform.SetParent(null, true);

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    } 

    private Vector3 GetMouseWorldPos()
    {
        // Convert mouse screen position to world coordinates
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void StartDragging()
    {
        transform.SetParent(null);

        mouseOffset = transform.position - GetMouseWorldPos();

        isDragging = true;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
