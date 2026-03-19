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

    private InputAction _lookAction;
    private float _pitch;   // вращение по X (вверх-вниз)
    private float _yaw;     // вращение по Y (влево-вправо)

    void Awake()
    {
        // Инлайн-экшн, не требует Asset
        _lookAction = new InputAction(
            "Look",
            binding: "<Mouse>/delta"
        );
    }

    void OnEnable()
    {
        _lookAction.Enable();
        // Берём текущие углы, чтобы не дёргать камеру при старте
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;
    }

    void OnDisable() => _lookAction.Disable();

    void Update()
    {
        Vector2 delta = _lookAction.ReadValue<Vector2>();
        if (delta.sqrMagnitude < 0.01f) return; // нет движения — пропускаем

        _yaw += delta.x * sensitivityX;
        _pitch -= delta.y * sensitivityY;        // минус = инвертируем ось Y
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
        _yaw = Mathf.Clamp(_yaw, minYaw, maxYaw);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }
}