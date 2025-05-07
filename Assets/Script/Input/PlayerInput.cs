using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions inputActions;

    public event UnityAction<Vector2> onMove;
    public event UnityAction onStopMove;

    public event UnityAction onFire;
    public event UnityAction onStopFire;

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    public void EnablePlayerInput()
    {
        inputActions.Player.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableAllInput()
    {
        inputActions.Player.Disable();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onFire != null)
                onFire.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            if (onStopFire != null)
                onStopFire.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (onMove != null)
                onMove.Invoke(context.ReadValue<Vector2>());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            if (onStopMove != null)
                onStopMove.Invoke();
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}
