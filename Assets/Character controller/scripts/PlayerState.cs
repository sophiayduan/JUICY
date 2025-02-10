using UnityEngine;

namespace Charactercontroller{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState {get; private set;} = PlayerMovementState.Idling;
        public enum PlayerMovementState {
            Idling = 0,
            Walking = 1,
            Running = 2,
            Sprinting = 3,
            Jumping = 4,
            Falling = 5,
            Strafing = 6,
        }
    }

}
