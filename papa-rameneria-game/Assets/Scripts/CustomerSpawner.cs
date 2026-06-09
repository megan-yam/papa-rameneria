using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] customerPrefabs;
    [SerializeField] private Transform queueSpot;
    public Transform spawnPoint;
    public float spawnInterval = 180f;
    private float timer;
    private int customerIdx = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    void SpawnCustomer()
    {
        if (customerIdx >= customerPrefabs.Length)
        {
            return;
        }
        GameObject customerObj =
            Instantiate(customerPrefabs[customerIdx],
                        spawnPoint.position,
                        Quaternion.Euler(-90f, 180f, 90f),
                        transform);
        Debug.Log("SetTarget called on " + gameObject.name);
        customerObj.transform.localScale = new Vector3(40f, 40f, 40f);
        Customer customer = customerObj.GetComponentInChildren<Customer>();
        if (customer == null)
        {
            Debug.LogError("Customer script not found on prefab!");
            return;
        }

        if (queueSpot == null)
        {
            Debug.LogError("QueueSpot not assigned in Inspector!");
            return;
        }
        customer.SetTarget(queueSpot.position);
        customerIdx++;
    }
}
