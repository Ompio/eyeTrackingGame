using UnityEngine;

[ExecuteInEditMode]
public class CircularObjectPlacement : MonoBehaviour
{
    public int numberOfObjects = 10; // Liczba obiektów do rozmieszczenia
    public float radius = 5.0f; // Promień okręgu

    public GameObject objectToPlace;
    public bool run;

    private bool isInPlayMode = false;

    private int childCount = 0;

    void Update()
    {
        if (run)
        {
            isInPlayMode = Application.isPlaying;
            if (!isInPlayMode)
            {
                RemoveChildren(transform);
                PlaceObjectsInCircle(transform);
            }
        }
    }

    void RemoveChildren(Transform parent)
    {
        GameObject newParent = new GameObject("DetachedObjects");

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            // Ustawienie obiektu jako niewidoczny
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            // Przypisanie nowego rodzica dla dziecka
            child.SetParent(newParent.transform);
        }

        DestroyImmediate(newParent);
    }

    void PlaceObjectsInCircle(Transform parent)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            GameObject newObject = Instantiate(objectToPlace, transform.position + position, Quaternion.identity);
            newObject.transform.SetParent(transform);
        }
        childCount = parent.childCount;
    }
}
