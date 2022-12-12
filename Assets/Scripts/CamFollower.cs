using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

   // [SerializeField] private float smoothSpeed = 0.04f;

    public Vector3 offset;

    public float positionOffset;

    private Vector3 velocity;

    public float SmoothDump;

    void FixedUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        if (player.position.y < transform.position.y + positionOffset)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition , ref velocity , SmoothDump);
        }
    }
}
