// -- Human Archer Animations 2.0 | Kevin Iglesias --
// This script is a secondary script that works with HumanArcherController.cs script.
// It animates the bow when entering or exiting an AnimatorController state.
// You can freely edit, expand, and repurpose it as needed. To preserve your custom changes when updating
// to future versions, it is recommended to work from a duplicate of this script.

// Contact Support: support@keviniglesias.com

using UnityEngine;

namespace KevinIglesias
{
    public enum BowConditions
    {
        OnEnter,
        OnExit
    }
    
    public enum BowActions
    {
        Pull,
        Release,
        Cancel
    }
    
    public class HumanArcherBowSMB : StateMachineBehaviour
    {
        public float attackSpeed = 1f;

        public BowConditions condition;
        
        public BowActions bowAction;
        
        public float delay;
        
        public float duration;
        
        private HumanArcherController hAC;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(condition == BowConditions.OnEnter)
            {
                if(!hAC)
                {
                    hAC = animator.GetComponent<HumanArcherController>();
                    attackSpeed = animator.GetFloat("AttackSpeed");
                }
                
                if(bowAction == BowActions.Pull)
                {
                    hAC.LoadBow(delay / attackSpeed, duration);
                }else{
                    hAC.ShootArrow(delay / attackSpeed, duration);
                }
            }
        }

        // OnStateExit is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(condition == BowConditions.OnExit)
            {
                if(!hAC)
                {
                    hAC = animator.GetComponent<HumanArcherController>();
                }
                
                if(bowAction == BowActions.Pull)
                {
                    hAC.LoadBow(delay, duration);
                }else{
                    hAC.ShootArrow(delay, duration);
                }
            }
        }
    }
}
