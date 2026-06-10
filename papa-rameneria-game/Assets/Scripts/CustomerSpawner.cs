using UnityEngine;
 
public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] customerPrefabs;
    [SerializeField] private Transform queueSpot;
    public Transform spawnPoint;
    
    private int customerIdx = 0;
    private bool isCounterOccupied = false;

    // TRACKER: Tracks how many total customers have entered the scene
    private int totalSpawnedCount = 0;

    void Start()
    {
        // Spawn the very first customer immediately when the level begins
        SpawnNextCustomer();
    }

    // Public method that your serve button calls
    public void NotifyCustomerServed()
    {
        isCounterOccupied = false;
        
        // Spawn the next person in line
        SpawnNextCustomer();
    }
 
    void SpawnNextCustomer()
    {
        // 1. FINITE CHECK: If we have already spawned all 5 customers, stop here!
        if (totalSpawnedCount >= customerPrefabs.Length)
        {
            Debug.Log("<color=green><b>ALL CUSTOMERS SERVED! LEVEL COMPLETE!</b></color>");
            return;
        }

        // Safety lock: Don't spawn if someone is still currently at the counter
        if (isCounterOccupied) return;

        if (customerPrefabs == null || customerPrefabs.Length == 0)
        {
            Debug.LogError("No customer prefabs assigned to CustomerSpawner!");
            return;
        }
 
        // Create the physical customer structure
        GameObject customerObj = Instantiate(
            customerPrefabs[customerIdx],
            spawnPoint.position,
            Quaternion.Euler(-90f, 180f, 90f),
            transform
        );
 
        customerObj.transform.localScale = new Vector3(40f, 40f, 40f);
        Customer customer = customerObj.GetComponentInChildren<Customer>();
 
        if (customer == null)
        {
            Debug.LogError($"Customer script missing on prefab '{customerPrefabs[customerIdx].name}'!");
            Destroy(customerObj);
            return;
        }
 
        Debug.Log($"Spawning customer {totalSpawnedCount + 1}/{customerPrefabs.Length}: {customerObj.name}");
        customer.SetTarget(queueSpot.position);
        
        // Mark the station as busy
        isCounterOccupied = true;

        // Increment our trackers
        totalSpawnedCount++;
        customerIdx++; // Moves to the next index linearly (0 -> 1 -> 2 -> 3 -> 4)
    }
}