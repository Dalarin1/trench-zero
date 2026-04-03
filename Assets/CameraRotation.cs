using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [Header("Sensitivity")]
    public float sensitivityX = 0.15f;
    public float sensitivityY = 0.15f;

    [Header("Vertical clamp")]
    public float minPitch = -20f;
    public float maxPitch = 20f;

    public float minYaw = -30f;
    public float maxYaw = 30f;

    public InputActionReference lookAction;
    private float pitch;   // вращение по X (вверх-вниз)
    private float yaw;     // вращение по Y (влево-вправо)


    void OnEnable()
    {
        lookAction.action.Enable();
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void OnDisable() => lookAction.action.Disable();

    void Update()
    {
        Vector2 delta = lookAction.action.ReadValue<Vector2>();
        if (delta.sqrMagnitude < 0.01f) return;

        yaw += delta.x * sensitivityX;
        pitch -= delta.y * sensitivityY; 

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}