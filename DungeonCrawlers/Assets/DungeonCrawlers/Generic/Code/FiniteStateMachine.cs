using System;
using UnityEditor.Animations;
using UnityEngine;

namespace DungeonCrawlers.Agent
{
    #region Enums

    public enum FiniteStates
    {
        IDLE,
        MOVING,
        ATTACKING,
        DEATH
    }

    public enum StateMechanics //bool parameters from the animator
    {
        STOP,
        MOVE,
        ATTACK,
        DIE
    }

    public enum StateDirection
    {
        DOWN, //0
        UP,   //1
        LEFT, //2
        RIGHT //3
    }

    #endregion

    #region Structs

    [System.Serializable]
    public struct DirectionAnimatorControllers
    {
        [SerializeField] public RuntimeAnimatorController up;
        [SerializeField] public RuntimeAnimatorController down;
        [SerializeField] public RuntimeAnimatorController left;
        [SerializeField] public RuntimeAnimatorController right;
    }

    #endregion

    [RequireComponent(typeof(Animator))]
    public class FiniteStateMachine : MonoBehaviour
    {
        #region Parameters

        [SerializeField] public DirectionAnimatorControllers directionAnimatorControllers;

        #endregion

        #region References

        [SerializeField] protected Animator _animator;

        #endregion

        #region RuntimeVariables

        [SerializeField] protected FiniteStates _currentFiniteState;
        [SerializeField] protected StateDirection _currentStateDirection; //= StateDirection.DOWN;

        #endregion

        #region PublicMethods

        public void EnteredStateFromStateMachineBehaviour(FiniteStates value)
        {
            CleanAnimatorFlagParameters();
            _currentFiniteState = value;
        }

        public void StateMechanic(StateMechanics value)
        {
            _animator.SetBool(value.ToString(), true);
        }

        public void StateMechanic(StateMechanics stateMechanic, StateDirection direction)
        {
            StateMechanic(stateMechanic);
            _currentStateDirection = direction;
            switch (direction)
            {
                case StateDirection.DOWN:
                    _animator.runtimeAnimatorController = directionAnimatorControllers.down;
                    break;
                case StateDirection.UP:
                    _animator.runtimeAnimatorController = directionAnimatorControllers.up;
                    break;
                case StateDirection.LEFT:
                    _animator.runtimeAnimatorController = directionAnimatorControllers.left;
                    break;
                case StateDirection.RIGHT:
                    _animator.runtimeAnimatorController = directionAnimatorControllers.right;
                    break;
            }
        }

        #endregion

        #region LocalMethods

        protected void CleanAnimatorFlagParameters()
        {
            foreach (StateMechanics stateMechanic in (StateMechanics[])Enum.GetValues(typeof(StateMechanics)))
            {
                _animator.SetBool(stateMechanic.ToString(), false);
            }
        }

        #endregion
    }
}

