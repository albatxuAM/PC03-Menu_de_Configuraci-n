using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float potenciaSalto = 5f;

    private Rigidbody rb;
    private Vector2 movimientoInput;
    private bool saltoInput;

    private CustomControls inputControls;

    public PauseMenuController pauseMenuController;

    private void Awake()
    {
        // Crear una instancia del archivo Input Actions
        inputControls = new CustomControls();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Activa el Action Map "Player"
        inputControls.PlayerInput.Enable();

        // Suscribirse a los eventos de input
        inputControls.PlayerInput.Move.performed += OnMove;
        inputControls.PlayerInput.Move.canceled += OnMove;
        inputControls.PlayerInput.Jump.performed += OnJump;

        // Activa el Action Map "Pause"
        inputControls.PauseInput.Enable();

        inputControls.PauseInput.Pause.performed += pauseMenuController.OnPause;
    }

    private void OnDisable()
    {
        // Desactiva el Action Map "Player"
        inputControls.PlayerInput.Disable();

        // Desuscribirse de los eventos de input
        inputControls.PlayerInput.Move.performed -= OnMove;
        inputControls.PlayerInput.Move.canceled -= OnMove;
        inputControls.PlayerInput.Jump.performed -= OnJump;

        // Activa el Action Map "Pause"
        inputControls.PauseInput.Disable();

        inputControls.PauseInput.Pause.performed -= pauseMenuController.OnPause;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && rb.velocity.normalized.y == 0)
        {
            saltoInput = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 mover = new Vector3(movimientoInput.x, 0, movimientoInput.y) * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + mover);

        if (saltoInput)
        {
            rb.AddForce(Vector3.up * potenciaSalto, ForceMode.Impulse);
            saltoInput = false;
        }
    }

    public void CambiarVelocidad(float nuevaVelocidad)
    {
        velocidadMovimiento = nuevaVelocidad;
    }

    public float GetVelocidad()
    {
        return velocidadMovimiento;
    }

    public void CambiarSalto(float nuevoSalto)
    {
        potenciaSalto = nuevoSalto;
    }

    public float GetSalto()
    {
        return potenciaSalto;
    }
}
