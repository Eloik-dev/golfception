using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class ClubController : MonoBehaviour
{
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;
    [SerializeField] GameState gameState;
    [SerializeField] GameObject body;
    [Range(0.01f, 2f)] public float positionSmoothTime = 0.1f;
    [Range(0.01f, 2f)] public float rotationSmoothTime = 0.2f;

    private Transform controller;
    private InputDevice inputController;
    private bool controllerFound = false;
    private bool isControllerTracked = false;

    private Vector3 initialBodyPosition = new Vector3(0, -10.1f, 92.8f);
    private float yOffsetConstant = 0.425742574f;
    private float zOffsetConstant = 0.360991379f;

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
        checkForUpdatedPreference();
        checkForControllerFound();
        updateOffset();
    }

    private void checkForUpdatedPreference()
    {
        controller = gameState.GetRightHanded() ? rightController : leftController;
    }

    private void checkForControllerFound()
    {
        if (!controllerFound)
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

            if (devices.Count > 0)
            {
                inputController = devices[0];
                controllerFound = true;
            }
        }
        else
        {
            bool isTracked = false;
            if (inputController.TryGetFeatureValue(CommonUsages.isTracked, out isTracked))
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

    private void updateOffset()
    {
        float offsetHeight = gameState.GetOffsetHeight();
        float newY = initialBodyPosition.y * (yOffsetConstant / offsetHeight);
        float newZ = initialBodyPosition.z * (zOffsetConstant / offsetHeight);
        
        body.transform.localPosition = new Vector3(0, newY, newZ);
    }
}
