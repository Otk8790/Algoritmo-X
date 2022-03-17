using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    public Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
    }
}
