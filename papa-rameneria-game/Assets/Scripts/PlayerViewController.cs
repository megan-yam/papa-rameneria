using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    public enum Station
    {
        OrderStation,
        RamenStation,
        SoupStation,
        ToppingStation
    }

    public Station currentView;

    public void FaceOrderStation()
    {
        currentView = Station.OrderStation;
        transform.rotation = Quaternion.Euler(0, 0, 38.59f);
        transform.position = new Vector3(2.8f, 0.7f, 1.7f);
    }

    public void FaceRamenStation()
    {
        currentView = Station.RamenStation;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = new Vector3(-1.59f, 1.13f, 1.22f);
    }

    public void FaceSoupStation()
    {
        currentView = Station.SoupStation;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = new Vector3(-1.281f, 1.21f, 3.954f);
    }

    public void FaceToppingStation()
    {
        currentView = Station.ToppingStation;
        transform.rotation = Quaternion.Euler(0, 270, 0);
        transform.position = new Vector3(-1.23f, 1.08f, 4.38f);
    }
}