using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Sends user input to the correct control systems.
/// </summary>
public class InputController : MonoBehaviour
{
    public float stepSize = 0.1f;

    PlayerInputActions playerInputActions;

    public enum State
    {
        CharacterControl,
        Pause,
    }

    State state;

    public void ChangeState(State state) => this.state = state;
    public State GetState() => this.state;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActions.Disable();        

        playerInputActions.PlayerActions.Movement.performed += Movement_Performed;
        playerInputActions.PlayerActions.Movement.canceled += Movement_Canceled;

        playerInputActions.PlayerActions.Enable();

        state = State.CharacterControl;
    }

    //player keyboard controller
    void Movement_Performed(InputAction.CallbackContext context)
    {
        if (AppManager.gameplayState == AppManager.GameplayState.play)
        {
            if (state == State.CharacterControl)
            {
                if (context.performed)
                {
                    Vector2 vector2 = playerInputActions.PlayerActions.Movement.ReadValue<Vector2>();
                    //Debug.Log(vector2);
                    AppManager.playerController.startMoving(vector2);
                }
            }
        }
    }

    void Movement_Canceled(InputAction.CallbackContext context)
    {
        if (AppManager.gameplayState == AppManager.GameplayState.play)
        {
            if (state == State.CharacterControl)
            {
                //AppManager.playerController.stopMoving(Vector2.zero);
            }
        }
    }


}
