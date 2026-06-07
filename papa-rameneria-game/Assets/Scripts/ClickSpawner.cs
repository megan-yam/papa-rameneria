using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    // Drag your prefab into this field in the Unity Inspector
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public void OnMouseDown()
    {
        if(prefabToSpawn != null && spawnPoint != null)
        {
            GameObject spawned = Instantiate(prefabToSpawn, spawnPoint.position, transform.rotation);
            spawned.transform.localScale = transform.lossyScale;
            Debug.Log(spawned.transform.lossyScale);
        }
        
    }
}
