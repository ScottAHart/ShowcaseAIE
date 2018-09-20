using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Animation behaviour to increment or set hit index on player controller
/// </summary>
public class HitBehaviour : StateMachineBehaviour {
    public int _hitIndex = 0;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<PlayerControl>().SetHit(_hitIndex);
	}
}
