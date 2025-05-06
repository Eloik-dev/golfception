using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class ClubController : MonoBehaviour
{
    [SerializeField] Transform controller;
    [SerializeField] GameState gameState;
    [Range(0.01f, 2f)] public float positionSmoothTime = 0.1f;
    [Range(0.01f, 2f)] public float rotationSmoothTime = 0.2f;

    private InputDevice rightController;
    private bool controllerFound = false;
    private bool isControllerTracked = false;

    Rigidbody rb;
    Renderer clubRenderer;
    Vector3 positionVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        clubRenderer = GetComponentInChildren<Renderer>();
    }

    void FixedUpdate()
    {
        if (isControllerTracked)
        {
            // Smooth position via physics
            Vector3 targetPos = controller.position;
            Vector3 newPos = Vector3.SmoothDamp(rb.position, targetPos, ref positionVelocity, positionSmoothTime);
            rb.MovePosition(newPos);

            // Smooth rotation via physics
            Quaternion targetRot = controller.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, Time.fixedDeltaTime / rotationSmoothTime));
        }
    }


    void Update()
    {
        if (!controllerFound)
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

            if (devices.Count > 0)
            {
                rightController = devices[0];
                controllerFound = true;
            }
        }
        else
        {
            bool isTracked = false;
            if (rightController.TryGetFeatureValue(CommonUsages.isTracked, out isTracked))
            {
                isControllerTracked = isTracked;
            }
            else
            {
                isControllerTracked = false;
            }
        }

        controller.gameObject.SetActive(controllerFound);
        clubRenderer.material.SetColor("_BaseColor", gameState.GetClubColor());
    }
}
