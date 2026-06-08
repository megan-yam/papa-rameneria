using UnityEngine;

public class CustomerArrive : MonoBehaviour
{
    private GameObject customerPrefab;
    private Transform spawnPoint;
    public float spawnInterval = 300f;
    private float timer;

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
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
    }
}
