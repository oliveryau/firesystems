using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float smoothing;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (transform.position != player.position)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z); //(player.x, player.y, camera.z)
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x); //set x boundary
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); //set y boundary
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
