using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    public Camera cam;

    private PlayerInput _input;
    private InputAction _clickAction;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();          
        _clickAction = _input.actions["Click"];
    }

    void OnEnable() => _clickAction.performed += OnClick;
    void OnDisable() => _clickAction.performed -= OnClick;

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("ExitDoor"))
                Application.Quit();
        }
    }
}