using UnityEngine;

public class BowlSpawner : MonoBehaviour
{
    public GameObject bowlPrefab;

    public Transform holdPoint;

    public Transform orderStationDrop;
    public Transform soupStationDrop;
    public Transform ramenStationDrop;
    public Transform toppingStationDrop;

    public PlayerViewController player;
    public Transform spawnPoint;

    public void OnMouseDown()
    {
        if(bowlPrefab != null && spawnPoint != null)
        {
            GameObject bowlObj = Instantiate(bowlPrefab, spawnPoint.position, transform.rotation);

            BowlPickup bowl = bowlObj.GetComponent<BowlPickup>();

            bowl.holdPoint = holdPoint;
            bowl.orderStationDrop = orderStationDrop;
            bowl.soupStationDrop = soupStationDrop;
            bowl.ramenStationDrop = ramenStationDrop;
            bowl.toppingStationDrop = toppingStationDrop;
            bowl.player = player;
        }
    }
}