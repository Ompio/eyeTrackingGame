using UnityEngine;

public class ScaleWithDistance : MonoBehaviour
{
    public Transform cameraTransform; // Ustaw tu kamerę lub inny obiekt referencyjny
    public float scaleFactor = 0.1f;  // Współczynnik skalowania

    private Vector3 initialScale;     // Początkowa skala obiektu

    void Start()
    {
        // Zapisz początkową skalę obiektu
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Oblicz odległość od kamery do obiektu
        float distance = Vector3.Distance(transform.position, cameraTransform.position);
        distance = distance - 5;
        Debug.Log("distance: "+distance);
        // Zmieniaj skalę obiektu w zależności od odległości
        transform.localScale = initialScale * (1 + scaleFactor * distance);
    }
}
