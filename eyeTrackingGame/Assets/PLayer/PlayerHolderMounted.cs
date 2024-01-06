using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolderMounted : MonoBehaviour
{
    public Transform playerPosition;
    // Update is called once per frame
    void MountPlayer()
    {
        transform.position = playerPosition.position;
    }
}
