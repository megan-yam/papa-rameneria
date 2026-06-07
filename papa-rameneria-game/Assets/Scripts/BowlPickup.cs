using UnityEngine;
using UnityEngine.EventSystems;

public class BowlPickup : MonoBehaviour
{
    public Transform holdPoint;

    public Transform northDrop;
    public Transform eastDrop;
    public Transform southDrop;
    public Transform westDrop;
    private Quaternion originalRotation;

    public PlayerViewController player;

    private bool isHeld = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;

        if (!isHeld)
            PickUp();
        else
            PutDown();
    }

    void PickUp()
    {
        isHeld = true;

        transform.SetParent(holdPoint);

        transform.localPosition = Vector3.zero;
        transform.rotation = originalRotation; 
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void PutDown()
    {
        isHeld = false;

        Transform targetDrop = GetDropZone();

        transform.SetParent(null);

        transform.position = targetDrop.position;
        transform.rotation = originalRotation; 

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    Transform GetDropZone()
    {
        switch (player.currentView)
        {
            case PlayerViewController.ViewDirection.North:
                return northDrop;

            case PlayerViewController.ViewDirection.East:
                return eastDrop;

            case PlayerViewController.ViewDirection.South:
                return southDrop;

            case PlayerViewController.ViewDirection.West:
                return westDrop;
        }

        return northDrop;
    }
}