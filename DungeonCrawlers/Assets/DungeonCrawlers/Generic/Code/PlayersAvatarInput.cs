using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawlers.Agent
{
    public class PlayersAvatarInput : MonoBehaviour
    {
        #region References

        [SerializeField] protected FiniteStateMachine _fsm;

        #endregion

        #region UnityMethods 

        //Update() in Editor Mode
        public void OnDrawGizmos()
        {
            if (_fsm == null)
            {
                _fsm = GetComponent<FiniteStateMachine>();
            }
        }

        #endregion

        #region InputActions

        public void OnMove(InputAction.CallbackContext value)
        {
            // es para moverme por medio del inputSystem
            if (value.performed)
            {
                _fsm.StateMechanic(StateMechanics.MOVE, GetStateDirection(value.ReadValue<Vector2>()));
            }
            if (value.canceled)
            {
                _fsm.StateMechanic(StateMechanics.STOP);
            }
        }

        public void OnAttack(InputAction.CallbackContext value)
        {

        }

        public void OnInteract(InputAction.CallbackContext value)
        {

        }

        public void OnShootArrow(InputAction.CallbackContext value)
        {

        }

        public void OnPlaceBomb(InputAction.CallbackContext value)
        {

        }

        #endregion

        #region LocalMethods

        protected StateDirection GetStateDirection(Vector2 value)
        {
            if (Vector2.Dot(Vector2.down, value) >= 0.5f)
            {
                return StateDirection.DOWN;
            }
            else if (Vector2.Dot(Vector2.left, value) >= 0.5f)
            {
                return StateDirection.LEFT;
            }
            else if (Vector2.Dot(Vector2.right, value) >= 0.5f)
            {
                return StateDirection.RIGHT;
            }
            //else if (Vector2.Dot(Vector2.up, value) >= 0.5f)
            //{
            return StateDirection.UP;
            //}
        }

        #endregion
    }
}