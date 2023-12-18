using UnityEngine;

[ExecuteInEditMode]
public class AutoObjectPlacement : MonoBehaviour
{
    public int numberOfObjectsX = 5;
    public int numberOfObjectsY = 5;
    public int numberOfObjectsZ = 5;

    public float spacingX = 2.0f;
    public float spacingY = 2.0f;
    public float spacingZ = 2.0f;

    public GameObject objectToPlace;
    public bool run;

    private bool isInPlayMode = false;

    private int childCount = 0;

    void Update()
    {   
        if(run){
            isInPlayMode = Application.isPlaying;   
            if (!isInPlayMode)
            {
                RemoveChildren(transform);
                PlaceObjects(transform);
            }
        }
    }
    // Ta metoda jest wywoływana automatycznie, gdy zmieniasz wartości parametrów w Unity Editor

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

    void PlaceObjects(Transform parent)
    {
        for (int x = 0; x < numberOfObjectsX; x++)
        {
            for (int y = 0; y < numberOfObjectsY; y++)
            {
                for (int z = 0; z < numberOfObjectsZ; z++)
                {
                    Vector3 position = new Vector3(x * spacingX, y * spacingY, z * spacingZ);

                    GameObject newObject = Instantiate(objectToPlace, transform.position + position, Quaternion.identity);
                    newObject.transform.SetParent(transform);
                }
            }
        }
        childCount = parent.childCount;
    }
}
