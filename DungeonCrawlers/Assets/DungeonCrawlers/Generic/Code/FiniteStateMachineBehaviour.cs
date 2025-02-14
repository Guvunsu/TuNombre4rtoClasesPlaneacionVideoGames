using UnityEngine;

namespace DungeonCrawlers.Agent
{
    public class FiniteStateMachineBehaviour : StateMachineBehaviour
    {
        #region Parameters

        public FiniteStates finiteState;

        #endregion

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //obtiene el fsm del objeto "attach", chismosea al metodo para reiniciar o limpiar las animaciones y sincroniza las animaciones del juego 
            animator.gameObject.GetComponent<FiniteStateMachine>().EnteredStateFromStateMachineBehaviour(finiteState);
        }
    }
}