using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    public enum ViewDirection
    {
        North,
        East,
        South,
        West
    }

    public ViewDirection currentView;

    public void FaceNorth()
    {
        currentView = ViewDirection.North;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(2.35f, 0.8f, 1.7f);
    }

    public void FaceEast()
    {
        currentView = ViewDirection.East;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        transform.position = new Vector3(-0.929f, 1.203f, 0.838f);
    }

    public void FaceSouth()
    {
        currentView = ViewDirection.South;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = new Vector3(-1.305f, 1.203f, 3.065f);
    }

    public void FaceWest()
    {
        currentView = ViewDirection.West;
        transform.rotation = Quaternion.Euler(0, 270, 0);
        transform.position = new Vector3(-0.37f, 1.16f, 3.79f);
    }
}