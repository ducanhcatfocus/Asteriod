using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerPos; 


    void Update()
    {
        if (playerPos != null)
        {
            Vector3 targetPosition = playerPos.position;
            targetPosition.z = -10;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }
}
