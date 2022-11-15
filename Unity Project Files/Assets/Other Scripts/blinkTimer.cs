using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkTimer : StateMachineBehaviour
{
    public string parameterName;
    public int resetValue;
    public string otherBlinkState;
    public int layer;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetInteger(parameterName) > resetValue)
        {
            animator.SetInteger(parameterName, 0);
            animator.Play(otherBlinkState, layer);
        }
        else
        {
            animator.SetInteger(parameterName, animator.GetInteger(parameterName) + 1);
        }
    }
}
