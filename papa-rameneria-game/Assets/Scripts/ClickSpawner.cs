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
            GameObject spawned = Instantiate(prefabToSpawn, spawnPoint.position, prefabToSpawn.transform.rotation);
            // spawned.transform.localScale = transform.lossyScale;

            DragObjects drag = spawned.GetComponent<DragObjects>();

            if (drag != null)
            {
                drag.StartDragging();
            }
            
        }
        
    }
}
