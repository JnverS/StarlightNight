using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetZ;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float speed;
    private Vector3 pos;

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Hero>().transform;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            pos = player.position;
            Vector3 newPos = new Vector3(pos.x + offsetX, pos.y + offsetY, offsetZ);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        }
    }
}
