using UnityEngine;
using UnityEngine.EventSystems;

public class BowlPickup : MonoBehaviour
{
    public Transform holdPoint;

    public Transform orderStationDrop;
    public Transform soupStationDrop;
    public Transform ramenStationDrop;
    public Transform toppingStationDrop;
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
        Debug.Log("Current View: " + player.currentView);
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
            case PlayerViewController.Station.OrderStation:
                return orderStationDrop;

            case PlayerViewController.Station.SoupStation:
                return soupStationDrop;

            case PlayerViewController.Station.RamenStation:
                return ramenStationDrop;

            case PlayerViewController.Station.ToppingStation:
                return toppingStationDrop;
        }

        return orderStationDrop;
    }
}