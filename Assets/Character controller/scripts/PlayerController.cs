using UnityEngine;
using UnityEngine.InputSystem;

namespace Charactercontroller {
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
      [Header("Components")]
      [SerializeField] private CharacterController _characterController;
      [SerializeField] private Camera _playerCamera;      
      
      [Header("Base Movement")]
      public float runSpeed = 4f;
      public float runAcceleration = 0.25f;
      public float sprintAcceleration = 0.5f;
      public float sprintSpeed = 7f;
      public float drag = 0.1f;
      public float movingThreshold = 0.01f;
      public float gravity = 25f;
      public float jumpSpeed = 1f;

      [Header("Camera Settings")]
      public float lookSenseH = 0.1f;
      public float lookSenseV = 0.1f;
      public float lookLimitV = 89f;
      
      private PlayerInputs _playerLocomotionInput;
      private PlayerState _playerState;
      private Vector2 _cameraRotation = Vector2.zero;
      private Vector2 _playerTargetRotation = Vector2.zero;

      private float _verticalVelocity = 0f;

      private void Awake(){
        _playerLocomotionInput = GetComponent<PlayerInputs>();
        _playerState = GetComponent<PlayerState>();
      }

      private void Update(){
        UpdateMovementState();
        HandleVerticalMovment();
        HandleLateralMovement();
      }
      
      private void UpdateMovementState(){
        bool isMovementInput = _playerLocomotionInput.MovementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprintng = _playerLocomotionInput.SprintToggledOn && isMovingLaterally;
        bool isGrounded = IsGrounded();

        PlayerMovementState lateralState = isSprintng ? PlayerMovementState.Sprinting :
                                           isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;
        _playerState.SetPlayerMovementState(lateralState);

        if (!isGrounded && _characterController.velocity.y >= 0f){
          _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
        }
        else if (!isGrounded && _characterController.velocity.y < 0f){
          _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
        }
      }

      private void HandleVerticalMovment(){
        bool isGrounded = _playerState.InGroundedState();
        if (isGrounded && _verticalVelocity < 0)
          _verticalVelocity = 0f;
        
        _verticalVelocity -= gravity * Time.deltaTime;

        if(_playerLocomotionInput.JumpPressed && isGrounded){
          _verticalVelocity += Mathf.Sqrt(jumpSpeed * 3 * gravity);
        }
      }

      private void HandleLateralMovement(){
        bool isSprintng = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isGrounded = _playerState.InGroundedState();

        float lateralAcceleration = isSprintng ? sprintAcceleration : runAcceleration;
        float clampLateralMagnitude = isSprintng ?sprintSpeed : runSpeed;

        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDirection * lateralAcceleration * Time.deltaTime;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, clampLateralMagnitude);
        newVelocity.y += _verticalVelocity;

        _characterController.Move(newVelocity * Time.deltaTime);
      }
      
      
      private void LateUpdate(){
        _cameraRotation.x += lookSenseH * _playerLocomotionInput.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenseV * _playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);

        _playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * _playerLocomotionInput.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);

        _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
      }

      private bool IsMovingLaterally(){
        Vector3 LateralVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.y);
        return LateralVelocity.magnitude > movingThreshold;
      }

      private bool IsGrounded(){
        return _characterController.isGrounded;
      }
    }
}
