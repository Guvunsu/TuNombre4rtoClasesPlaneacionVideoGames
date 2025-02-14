using System;
using UnityEditor.Animations;
using UnityEngine;

namespace DungeonCrawlers.Agent
{
    #region Enums

    public enum FiniteStates
    //Se utilizara para el BaseLayer de mi Animator para las transicciones de las animaciones de mi personaje 
    {
        IDLE,
        MOVING,
        ATTACKING,
        DEATH
    }

    public enum StateMechanics
    //bool parameters from the animator
    {
        STOP,
        MOVE,
        ATTACK,
        DIE
    }

    public enum StateDirection
    // esto se usara para un metodo para decirle que posicion tomara la animacion cuando se aprete un boton del input del jugador e indicarle en que Pos debe estar 
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
    // cuando este en runtime el AnimatorController sabra como debe representar la animacion cuando el usuario aprete un boton de direccion 
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
        //parametrizo el fsm de mi animatorcontroller 

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
            //cuando me este moviendo y lo deje de hacer se llamara el metodo que me reiniciara o
            //limpiara las animaciones y se buscara la siguiente animacion cuando el usuario haga otra cosa por medio del Input 
            CleanAnimatorFlagParameters();
            _currentFiniteState = value;
        }

        public void StateMechanic(StateMechanics value)
        {
            //me definira por un bool si se activa una accion de las animaciones en respuesta de la logica del juego por medio del input del jugador 
            _animator.SetBool(value.ToString(), true);
        }

        public void StateMechanic(StateMechanics stateMechanic, StateDirection direction)
        {
            // controla el fsm del jugador y con u bool definira si el jugador aprieta un input y depediendo de la direccion se ejecutara tal animacion 
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
            // itinera y pregunta por medio de un bool por medio de un enum el animator para luego reiniciarlo 
            foreach (StateMechanics stateMechanic in (StateMechanics[])Enum.GetValues(typeof(StateMechanics)))
            {
                _animator.SetBool(stateMechanic.ToString(), false);
            }
        }

        #endregion
    }
}

