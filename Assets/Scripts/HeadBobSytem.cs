using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private bool enableHeadBob = true;

    [Header("Headbob Settings")]
    [SerializeField, Range(0f, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0f, 30f)] private float frequency = 10f;
    [SerializeField] private float movementThreshold = 0.1f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private Transform cameraHolder = null;

    private CharacterController controller;
    private Vector3 initialCameraPos;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform not assigned.");
            enabled = false;
            return;
        }

        initialCameraPos = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (!enableHeadBob) return;

        if (ShouldHeadBob())
        {
            ApplyHeadBob();
        }
        else
        {
            ResetCameraPosition();
        }

        cameraTransform.LookAt(GetFocusPoint());
    }

    private bool ShouldHeadBob()
    {
        if (!controller.isGrounded) return false;

        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        return horizontalVelocity.magnitude > movementThreshold;
    }

    private void ApplyHeadBob()
    {
        float bobOffsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        float bobOffsetX = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;

        Vector3 bobPosition = new Vector3(bobOffsetX, bobOffsetY, 0);
        cameraTransform.localPosition = initialCameraPos + bobPosition;
    }

    private void ResetCameraPosition()
    {
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initialCameraPos, Time.deltaTime * 5f);
    }

    private Vector3 GetFocusPoint()
    {
        if (cameraHolder == null) return transform.position + transform.forward * 15f;

        Vector3 focusPoint = transform.position + Vector3.up * cameraHolder.localPosition.y;
        focusPoint += cameraHolder.forward * 15f;
        return focusPoint;
    }
}