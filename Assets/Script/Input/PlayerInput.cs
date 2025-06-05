using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputSystem_Actions.IPlayerActions, InputSystem_Actions.IPauseMenuActions
{
    InputSystem_Actions inputActions;

    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };

    public event UnityAction onFire = delegate { };
    public event UnityAction onStopFire = delegate { };

    public event UnityAction onDodge = delegate { };

    public event UnityAction onPause = delegate { };
    public event UnityAction onUnPause = delegate { };


    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        inputActions.PauseMenu.SetCallbacks(this);
    }

    void SwitchActionMap(InputActionMap actionMap, bool isUI)
    {
        inputActions.Disable();
        actionMap.Enable();

        if (isUI)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SwitchToDynamicUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    public void EnablePlayerInput() => SwitchActionMap(inputActions.Player, false);

    public void EnablePauseInput() => SwitchActionMap(inputActions.PauseMenu, true);
    public void DisableAllInput()
    {
        inputActions.Disable();
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
        if (context.phase == InputActionPhase.Performed)
        {
            onDodge.Invoke();
        }
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
        if (context.phase == InputActionPhase.Canceled)
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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onPause.Invoke();
        }
    }
    public void OnUnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onUnPause.Invoke();
        }
    }
}
