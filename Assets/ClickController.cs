using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    public Camera cam;

    public InputActionReference clickAction;


    void OnEnable() => clickAction.action.performed += OnClick;
    void OnDisable() => clickAction.action.performed -= OnClick;

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent<IInteractable>(out var clickable))
            {
                clickable.OnClick();
            }
        }
    }
}