using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 10.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 50.0f;
    public float dragSpeed = 10.0f;
    public Transform target;  // El objeto al que la cámara se enfocará
    public float focusDistance = 10.0f;  // La distancia a la que la cámara se ubicará del objetivo

    private Vector3 dragOrigin;

    void Update()
    {
        HandleRotation();
        HandleZoom();
        HandleDrag();
        HandleFocus();
    }

    void HandleRotation()
    {
        // Rotación de la cámara con Alt + Click izquierdo
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.eulerAngles += new Vector3(rotationY, rotationX, 0);
        }
    }

    void HandleZoom()
    {
        // Zoom de la cámara con la rueda del ratón
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Vector3 position = transform.position;
        position += transform.forward * scroll;
        float distance = Vector3.Distance(position, Vector3.zero);

        if (distance > minZoom && distance < maxZoom)
        {
            transform.position = position;
        }
    }

    void HandleDrag()
    {
        // Arrastre de la cámara con el click de la rueda del ratón
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragOrigin - currentMousePosition;
            dragOrigin = currentMousePosition;

            Vector3 move = new Vector3(difference.x * dragSpeed * Time.deltaTime, difference.y * dragSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.Self);
        }
    }

    void HandleFocus()
    {
        // Enfocar en el objeto al presionar la tecla F
        if (Input.GetKeyDown(KeyCode.F) && target != null)
        {
            FocusOnTarget();
        }
    }

    void FocusOnTarget()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * focusDistance;
        transform.LookAt(target);
    }
}
