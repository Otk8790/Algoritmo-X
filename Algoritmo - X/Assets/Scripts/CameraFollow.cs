using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform ObjSeguir;
    [SerializeField]
    private float velCamera;
    [SerializeField]
    private float sensibilidad = 150;

    private float mouseX;
    private float mouseY;
    private float rotY = 0;
    private float rotX = 0;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.z;
    }
    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * sensibilidad * Time.deltaTime;
        rotX += mouseY * sensibilidad * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -60, 60);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);

    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, ObjSeguir.position, velCamera * Time.deltaTime);
    }
}
