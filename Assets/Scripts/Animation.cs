using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator characterAnimator;

    public void SetAnimationState(string triggerName)
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned.");
        }
    }

    public void ResetToIdle()
    {
        if (characterAnimator == null)
        {
            Debug.LogWarning("Animator component is not assigned.");
            return;
        }

        foreach (AnimatorControllerParameter parameter in characterAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                characterAnimator.ResetTrigger(parameter.name);
            }
        }

        characterAnimator.Play("Idle", 0, 0f);
        characterAnimator.Update(0f);
    }
}
