using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static CustomControls inputControls;

    private void Awake()
    {
        if (inputControls == null)
        {
            inputControls = new CustomControls();
        }
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}
