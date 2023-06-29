using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing;

    private void LateUpdate()
    {
        if (transform.position != player.position)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z); //(player.x, player.y, camera.z)
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
