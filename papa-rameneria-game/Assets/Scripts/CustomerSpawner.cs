using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] customerPrefabs;
    private Transform spawnPoint;
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
        Instantiate(customerPrefabs[customerIdx], spawnPoint.position, Quaternion.identity);
        customerIdx++;
    }
}
