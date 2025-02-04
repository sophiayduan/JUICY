using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerinput;
    private PlayerInput.OnFootActions onfoot;
    private PlayerMotor motor;
    private PlayerLook look;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerinput = new PlayerInput();
        onfoot = playerinput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onfoot.jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from movement action
        motor.ProcessMove(onfoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        look.ProcessLook(onfoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onfoot.Enable();
    }
    private void OnDisable()
    {
        onfoot.Disable();
    }
}
