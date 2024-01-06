using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    // Ustaw tu obiekt gracza
    public GameObject triggerMesh;
    public GameObject player;
    // Ustaw tu pozycję waypointa, do której gracz ma być przenoszony
    public Transform waypoint;

    void OnTriggerEnter(Collider other)
    {   
        // Sprawdź, czy to gracz wszedł w kolizję z obiektem
        if (other.gameObject == triggerMesh)
        {
            // Przenieś gracza do waypointa
            player.transform.position = new Vector3(waypoint.position.x,waypoint.position.y+1,waypoint.position.z);
        }
    }
}