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
            animator.gameObject.GetComponent<FiniteStateMachine>().EnteredStateFromStateMachineBehaviour(finiteState);
        }
    }
}